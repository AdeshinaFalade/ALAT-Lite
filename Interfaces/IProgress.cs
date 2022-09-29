namespace ALAT_Lite.Classes
{
    public interface IProgress
    {
        public void CloseProgressDialog();
        public void ShowProgressDialog(string status);

    }
}