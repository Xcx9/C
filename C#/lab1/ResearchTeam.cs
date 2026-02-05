using System;

namespace Lab1_Variant3
{
    public class ResearchTeam
    {
        private string _researchTopic;
        private string _organizationName;
        private int _registrationNumber;
        private TimeFrame _researchDuration;
        private Paper[] _publications;

        public ResearchTeam(string researchTopic, string organizationName, int registrationNumber, TimeFrame researchDuration)
        {
            _researchTopic = researchTopic;
            _organizationName = organizationName;
            _registrationNumber = registrationNumber;
            _researchDuration = researchDuration;
            _publications = new Paper[0]; // Пустой массив
        }
        public ResearchTeam() : this("Default Topic", "Default Org", 0, TimeFrame.Year) { }

        public string ResearchTopic
        {
            get { return _researchTopic; }
            set { _researchTopic = value; }
        }

        public string OrganizationName
        {
            get { return _organizationName; }
            set { _organizationName = value; }
        }

        public int RegistrationNumber
        {
            get { return _registrationNumber; }
            set { _registrationNumber = value; }
        }

        public TimeFrame ResearchDuration
        {
            get { return _researchDuration; }
            set { _researchDuration = value; }
        }

        public Paper[] Publications
        {
            get { return _publications; }
            set { _publications = value ?? new Paper[0]; }
        }

        public Paper LatestPublication
        {
            get
            {
                if (_publications == null || _publications.Length == 0)
                    return null;

                Paper latest = _publications[0];
                for (int i = 1; i < _publications.Length; i++)
                {
                    if (_publications[i].PublicationDate > latest.PublicationDate)
                        latest = _publications[i];
                }
                return latest;
            }
        }

        public bool this[TimeFrame timeframe]
        {
            get { return _researchDuration == timeframe; }
        }

        public void AddPapers(params Paper[] newPapers)
        {
            if (newPapers == null || newPapers.Length == 0)
                return;

            int oldLength = _publications.Length;
            Array.Resize(ref _publications, oldLength + newPapers.Length);
            Array.Copy(newPapers, 0, _publications, oldLength, newPapers.Length);
        }

        public override string ToString()
        {
            string publicationsList = "No publications";
            if (_publications != null && _publications.Length > 0)
            {
                publicationsList = "";
                for (int i = 0; i < _publications.Length; i++)
                {
                    publicationsList += _publications[i].ToString();
                    if (i < _publications.Length - 1)
                        publicationsList += "\n";
                }
            }

            return $"Research Team: {_researchTopic}\n" +
                   $"Organization: {_organizationName}\n" +
                   $"Reg. №: {_registrationNumber}, Duration: {_researchDuration}\n" +
                   $"Publications:\n{publicationsList}";
        }

        public virtual string ToShortString()
        {
            return $"Research Team: {_researchTopic}, Org: {_organizationName}, " +
                   $"Reg. №: {_registrationNumber}, Duration: {_researchDuration}";
        }
    }
}