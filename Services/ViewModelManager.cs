using SecurePass.ViewModels;

namespace SecurePass.Services
{
    //TODO: Remove this crutch and do normal pagination
    public static class ViewModelManager
    {
        public static LoginVm LoginVm { get; set; }
        public static MainVm MainVm { get; set; }
        public static AccountsVm AccountsVm { get; set; }
        public static HistoryVm HistoryVm { get; set; }
        public static NotesVm NotesVm { get; set; }
    }
}