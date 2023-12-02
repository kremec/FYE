using Syncfusion.Windows.Shared;

namespace FYE
{
    public class MainWindowViewModel : NotificationObject
    {
        private double _močBlok1;
        public double MočBlok1
        {
            get { return _močBlok1; }
            set
            {
                if (_močBlok1 != value)
                {
                    _močBlok1 = value;
                    RaisePropertyChanged("MočBlok1");

                    MočBlok2 = Math.Max(MočBlok2, _močBlok1);
                    MočBlok3 = Math.Max(MočBlok3, _močBlok1);
                    MočBlok4 = Math.Max(MočBlok4, _močBlok1);
                    MočBlok5 = Math.Max(MočBlok5, _močBlok1);
                }
            }
        }

        private double _močBlok2;
        public double MočBlok2
        {
            get { return _močBlok2; }
            set
            {
                if (_močBlok2 != value)
                {
                    _močBlok2 = value;
                    RaisePropertyChanged("MočBlok2");

                    MočBlok2 = Math.Max(MočBlok2, _močBlok1);

                    MočBlok3 = Math.Max(MočBlok3, _močBlok2);
                    MočBlok4 = Math.Max(MočBlok4, _močBlok2);
                    MočBlok5 = Math.Max(MočBlok5, _močBlok2);
                }
            }
        }

        private double _močBlok3;
        public double MočBlok3
        {
            get { return _močBlok3; }
            set
            {
                if (_močBlok3 != value)
                {
                    _močBlok3 = value;
                    RaisePropertyChanged("MočBlok3");

                    MočBlok3 = Math.Max(MočBlok3, _močBlok2);

                    MočBlok4 = Math.Max(MočBlok4, _močBlok3);
                    MočBlok5 = Math.Max(MočBlok5, _močBlok3);
                }
            }
        }

        private double _močBlok4;
        public double MočBlok4
        {
            get { return _močBlok4; }
            set
            {
                if (_močBlok4 != value)
                {
                    _močBlok4 = value;
                    RaisePropertyChanged("MočBlok4");

                    MočBlok4 = Math.Max(MočBlok4, _močBlok3);

                    MočBlok5 = Math.Max(MočBlok5, _močBlok4);
                }
            }
        }

        private double _močBlok5;
        public double MočBlok5
        {
            get { return _močBlok5; }
            set
            {
                if (_močBlok5 != value)
                {
                    _močBlok5 = value;
                    RaisePropertyChanged("MočBlok5");

                    MočBlok5 = Math.Max(MočBlok5, _močBlok4);
                }
            }
        }

        public double MočBloka(int blok)
        {
            switch (blok)
            {
                case 1: return MočBlok1;
                case 2: return MočBlok2;
                case 3: return MočBlok3;
                case 4: return MočBlok4;
                case 5: return MočBlok5;
                default: return -1;
            }
        }

        private DateTime _datumOd;
        public DateTime DatumOd
        {
            get { return _datumOd; }
            set {
                if (_datumOd != value)
                {
                    _datumOd = value;
                    RaisePropertyChanged("DatumOd");
                }
            }
        }

        private DateTime _datumDo;
        public DateTime DatumDo
        {
            get { return _datumDo; }
            set
            {
                if (_datumDo != value)
                {
                    _datumDo = value;
                    RaisePropertyChanged("DatumDo");
                }
            }
        }

    }
}
