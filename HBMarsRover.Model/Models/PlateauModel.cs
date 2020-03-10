namespace HBMarsRover.Model
{
    public class PlateauModel : BaseModel, IPlateau
    {
        public PlateauModel()
        {
           
        }

        public PlateauModel(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
