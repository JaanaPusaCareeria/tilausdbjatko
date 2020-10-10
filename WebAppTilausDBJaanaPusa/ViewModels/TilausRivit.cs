namespace WebAppTilausDBJaanaPusa.ViewModels
{
    using System;
    public class TilausRivit
    {
        public int TilausID { get; set; }
        public int TilausriviID { get; set; }
        public int TuoteID { get; set; }
        public string TuoteNimi { get; set; }
        public int Maara { get; set; }
        public float Ahinta { get; set; }
    }
}