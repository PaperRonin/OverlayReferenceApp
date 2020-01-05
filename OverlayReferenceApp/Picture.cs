namespace OverlayReferenceApp
{
    public class Picture
    {

        public static class Movement
        {

            public static int MoveTo()
            {

                return 0;
            }

        }

        public static class Resizing
        {
            public static int ScaleUp()
            {

                return 0;
            }

            public static int ScaleDown()
            {

                return 0;
            }
        }

        private static class MovementBoundaries
        {
            public static void CorrectPosition()
            {
            }

            private static bool IsOutOfBoundaries()
            {
                return false;
            }

        }
    }
}
