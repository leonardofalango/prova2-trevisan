using System.Linq;
using System.Collections.Generic;

App.Run();

public class Controller
{
    public void Solve(IEnumerable<Piece> pieces)
    {
        Piece topLeftPiece = pieces.First(p => p.IsLeftTopPiece());
        topLeftPiece.SetPosition(0, 0);

        Piece currentPiece = topLeftPiece;
        int currentX = 0;
        for (int k = 0; k < 32; k++)
        {
            for (int i = 0; i < 18; i ++)
            {
                currentX += 1;
                var x = pieces.First(p => currentPiece.ConnectRight(p));
                x.SetPosition(currentX, 0);
                currentPiece = x;
            }
        }
        
    }
}