namespace BOL.DTOs
{
    public class AppointmentReportResponse
    {
        public int? Total { get; set; }
        public int? Finish { get; set; }
        public int? Cancel { get; set; }
        public int? InProgress { get; set; }
        public int? NoProgress { get; set; }
        public float? FinishPercent { get; set; }
        public float? CancelPercent { get; set; }
        public float? InProgressPercent { get; set; }
        public float? NoProgressPercent { get; set; }

    }
}
