using System;

namespace Tank2D_XNA
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Tank2D game = new Tank2D())
            {
                game.Run();
            }
        }
    }
#endif
}

