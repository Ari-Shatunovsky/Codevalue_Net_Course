namespace LinqToObj
{
    public class A
    {
        public int PropertyA { get; set; }
        public string PropertyB { get; set; }
        int[] PropertyC { get; set; }
        public string PropertyD { get; set; }

        public A()
        {
            PropertyC = new[] {1, 5, 6};
        }

        public override string ToString()
        {
            return $"A: {PropertyA}; B: {PropertyB}; C: {PropertyC}; D: {PropertyD}";
        }
    }
}