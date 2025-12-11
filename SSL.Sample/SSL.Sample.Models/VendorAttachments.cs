namespace SSL.Sample.SSL.Sample.Models
{
    public class VendorAttachments
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string FileName { get; set; }

        private string _displayName;

        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(_displayName)) return _displayName;

                if (FileName is not null)
                    return Path.GetFileNameWithoutExtension(this.FileName).Split("_shp_")[0] + Path.GetExtension(this.FileName);

                return "";
            }


            set
            {
                this._displayName = value;
            }
        }

    }
}
