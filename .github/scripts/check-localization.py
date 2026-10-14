"""Fails when localized AppStrings resources do not share the neutral resource keys."""
import sys
import xml.etree.ElementTree as ET
from pathlib import Path

RESOURCES = Path(__file__).resolve().parents[2] / "Resources"


def keys(path: Path) -> set[str]:
    return {node.get("name") for node in ET.parse(path).getroot().findall("data")}


neutral = keys(RESOURCES / "AppStrings.resx")
failed = False
for localized in sorted(RESOURCES.glob("AppStrings.*.resx")):
    current = keys(localized)
    missing, extra = sorted(neutral - current), sorted(current - neutral)
    if missing or extra:
        failed = True
        print(f"::error file=Resources/{localized.name}::missing={missing} extra={extra}")
    else:
        print(f"{localized.name}: {len(current)} keys OK")

sys.exit(1 if failed else 0)
