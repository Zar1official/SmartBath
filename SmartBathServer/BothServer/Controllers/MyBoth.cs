using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BothServer.Controllers
{
    [ApiController]
    [Route("api/getBoth")]
    public class MyBoth : ControllerBase
    {
        [HttpGet]
        public Both? GetMyBoth(long uId = 0)
        {
            if (uId == 0)
                return null;
            JsonManager manager = new JsonManager(uId);
            Both myBoth = manager.GetMyBoth();

            long time = myBoth.timeStamp;
            long nowTime = DateTime.Now.Ticks;

            myBoth.fillingProcent += Both.Constants.UpdateFillBothInfo(time, nowTime, (bool)myBoth.drainStatus, (bool)myBoth.craneActie);

            if (myBoth.fillingProcent < 0)
                myBoth.fillingProcent = 0;
            if (myBoth.fillingProcent > 100)
                myBoth.fillingProcent = 100;

            myBoth.timeStamp = nowTime;

            return myBoth;
        }
    }
}
