namespace _11112023ClassWork.Models.Home
{
    /* Модель з даними, необхідними для відображення сторінки 
     * /Home/Transfer
     */
    public record TransferViewModel
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public String ControllerName { get; set; } = null!;
        public TransferFormModel? FormModel { get; set; }
    }
}
