using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UWG
{
    public class simuParam
    {
        private string _epwFile;
        private string _xmlFile;
        private int _simuStartMonth;
        private int _simuStartDay;
        private int _simuDuration;


        public string fileNameTextBox
        {
            get { return _epwFile; }
            set 
            { 
                _epwFile = value;
                if (String.IsNullOrEmpty(_epwFile))
                {
                    throw new ApplicationException("Please select epw file");
                }
                if (!File.Exists(_epwFile))
                {
                    throw new ApplicationException("Please pick another file! The file " + _epwFile + " does not exist.");
                }
            }
        }

        public string xmlFile
        {
            get { return _xmlFile; }
            set 
            { 
                _xmlFile = value;
                if (String.IsNullOrEmpty(_xmlFile))
                {
                    throw new ApplicationException("Please select xml input file");
                }
                if (!File.Exists(_xmlFile))
                {
                    throw new ApplicationException("Please pick another file! The file " + _xmlFile + " does not exist.");
                }
            }
        }

        public string simuStartMonthValidate
        {
            get {return _simuStartMonth.ToString(); }
            set 
            {
                if (String.IsNullOrEmpty(_simuStartMonth.ToString()))
                {
                    throw new ApplicationException("Please select value");
                }
                if (!Int32.TryParse(value, out _simuStartMonth))
                {
                    throw new ApplicationException("Please input in number format");
                }
                if (_simuStartMonth <=0 || _simuStartMonth >12)
                {
                    throw new ApplicationException("Please pick month between 1 and 12");
                }
            }           
        }

        public string simuStartDayValidate
        {
            get { return _simuStartDay.ToString(); }
            set
            {
                if (String.IsNullOrEmpty(_simuStartDay.ToString()))
                {
                    throw new ApplicationException("Please select value");
                }
                if (!Int32.TryParse(value, out _simuStartDay))
                {
                    throw new ApplicationException("Please input in number format");
                }
                //for each month check number of days
                if (_simuStartMonth == 1 || _simuStartMonth == 3 || _simuStartMonth == 5 || _simuStartMonth == 7 || _simuStartMonth == 8 || _simuStartMonth == 10 || _simuStartMonth == 12)
                {
                    if (_simuStartDay <= 0 || _simuStartDay > 31)
                    {
                        throw new ApplicationException("Please pick a valid date");
                    }
                }
                if (_simuStartMonth == 4 || _simuStartMonth == 6 || _simuStartMonth == 9 || _simuStartMonth == 11)
                {
                    if (_simuStartDay <= 0 || _simuStartDay > 30)
                    {
                        throw new ApplicationException("Please pick a valid date");
                    }
                }
                if (_simuStartMonth == 2 )
                {
                    if (_simuStartDay <= 0 || _simuStartDay > 28)
                    {
                        throw new ApplicationException("Please pick a valid date");
                    }
                }
            }
        }

        public string simuDurationValidate
        {
            get { return _simuDuration.ToString(); }
            set
            {
                if (String.IsNullOrEmpty(_simuDuration.ToString()))
                {
                    throw new ApplicationException("Please select value");
                }
                if (!Int32.TryParse(value, out _simuDuration))
                {
                    throw new ApplicationException("Please input in number format");
                }
                if (_simuDuration <= 0 || _simuDuration > 365)
                {
                    throw new ApplicationException("Please pick duration of <= 365 days");
                }
            }
        }
    } //close class
} //close name space
