namespace BOL.DTOs
{
    public class AppointmentReportResponse
    {
        public int? Total { get; set; }
        public int? Finish { get; set; }
        public int? Cancel { get; set; }
        public int? InProgress { get; set; }
        public int? NoProgress { get; set; }
        public int? FinishPercent { get; set; }
        public int? CancelPercent { get; set; }
        public int? InProgressPercent { get; set; }
        public int? NoProgressPercent { get; set; }

    }
}
