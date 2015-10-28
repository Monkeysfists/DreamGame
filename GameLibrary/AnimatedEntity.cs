using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary
{
    class AnimatedEntity : TextureEntity
    {


        AnimatedEntity(GameHandler gameHandler, string id = "") : base(gameHandler, "", id, sheetIndex )
        {
            this.sheetIndex = sheetIndex
        }
    }
}
