namespace BlazorAppointmentSystem.Shared.Helpers
{
    public static class TimeHelpers
    {
        public static string FormatDuration(TimeSpan? duration)
        {
            if (duration == null)
                return string.Empty;

            var ts = duration.Value;
            var parts = new List<string>();

            if (ts.Hours > 0)
                parts.Add($"{ts.Hours} hour{(ts.Hours > 1 ? "s" : "")}");
            if (ts.Minutes > 0)
                parts.Add($"{ts.Minutes} minute{(ts.Minutes > 1 ? "s" : "")}");
            if (ts.Seconds > 0 && ts.Hours == 0) // optionally show seconds only if <1 hour
                parts.Add($"{ts.Seconds} second{(ts.Seconds > 1 ? "s" : "")}");

            return string.Join(", ", parts);
        }
    }
}
