using Microsoft.AspNetCore.Components.Forms;

namespace HowToSoftware.Site.Components.Shared;

public sealed class BriefDateInput : InputDate<DateTime?>
{
    protected override void Dispose(bool disposing)
    {
        // .NET 8 InputBase leaves its parsing messages in the EditContext on
        // disposal. Reset the input to its last valid value to clear only its
        // own parsing store when the wizard hides or unmounts this field.
        if (disposing && EditContext is not null)
            CurrentValueAsString = FormatValueAsString(CurrentValue);

        base.Dispose(disposing);
    }
}
