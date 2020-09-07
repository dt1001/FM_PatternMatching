namespace FM_PatternMatching
{
    public class PatternInput
    {
        public int PatternLength { get; set; }
        public string Str { get; set; }
        /// <summary>
        /// Ignores case senstivity when searching for patterns when set to true.
        /// default: true
        /// </summary>
        public bool IgnoreCase { get; set; } = true;
    }
}
