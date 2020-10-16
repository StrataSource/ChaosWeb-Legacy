namespace ChaosInitiative.Web.ControlPanel.Model
{
    public class Game
    {
        public string Name { get; set; }
        private string _repository;

        public string Repository
        {
            get => _repository;
            set
            {
                // Strip trailing slashes
                if (value.EndsWith("/"))
                    value = value.Substring(0, value.Length - 1);

                _repository = value;
            }
        }
    }
}