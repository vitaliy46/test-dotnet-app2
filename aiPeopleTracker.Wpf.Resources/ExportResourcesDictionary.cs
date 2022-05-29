namespace aiPeopleTracker.Wpf.Resources
{
    public partial class ExportResourcesDictionary
    {
        //Expose it as singleton to avoid multiple instances of this dictionary
        private static readonly ExportResourcesDictionary _instance = new ExportResourcesDictionary();

        public static ExportResourcesDictionary Instance
        {
            get { return _instance; }
        }

        public ExportResourcesDictionary()
        {
            InitializeComponent();
        }
    }
}