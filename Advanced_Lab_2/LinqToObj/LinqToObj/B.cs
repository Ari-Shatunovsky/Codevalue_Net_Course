namespace LinqToObj
{
    public class B
    {
        public string PropertyA { get; set; }
        public string PropertyB { get; set; }
        public int[] PropertyC { get; set; }
        public string PropertyD { get; set; }

        public override string ToString()
        {
            return $"A: {PropertyA}; B: {PropertyB}; C: {PropertyC}; D: {PropertyD}";
        }
    }
}