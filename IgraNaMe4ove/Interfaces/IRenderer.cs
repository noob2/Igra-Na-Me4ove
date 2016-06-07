namespace IgraNaMe4ove.Interfaces
{
    using System;

    public interface IRenderer
    {
        void WriteAt(Position position, string messege);

        void WriteAt(Position position, char messege, ConsoleColor colour);

        void Clear();

        void Colour(ConsoleColor colour);
    }
}
