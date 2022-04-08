using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BothServer.Controllers
{
    [ApiController]
    [Route("api/SetBoth")]
    public class Setter : Controller
    {
        /// <summary>
        /// перезаписать новые данные о ванне
        /// </summary>
        /// <param name="both"></param>
        /// <returns>true - всё ок, false - ошибка возникла</returns>
        [HttpPost]
        public Both? SetBoth(long uId, float? temp = null, Color? color = null, bool? drainStatus = null, bool? craneActive = null)
        {
            try
            {
                JsonManager manager = new JsonManager(uId);
                Both myBoth = manager.GetMyBoth();
                long time = myBoth.timeStamp;
                long nowTime = DateTime.Now.Ticks;
                myBoth.fillingProcent += Both.Constants.UpdateFillBothInfo(time, nowTime, (bool)myBoth.drainStatus, (bool)myBoth.craneActie);

                if (myBoth.fillingProcent < 0)
                    myBoth.fillingProcent = 0;
                if (myBoth.fillingProcent > 100)
                    myBoth.fillingProcent = 100;

                myBoth.timeStamp = nowTime; //текущее время

                ///обновить данные о статусе заполнения(используя информацию о времени)

                if (temp != null && temp >= Both.Constants.MinTemp && temp <= Both.Constants.MaxTemp)
                    myBoth.temp = temp;
                if (color != null)
                    myBoth.waterColor = color;
                if (drainStatus != null)
                    myBoth.drainStatus = drainStatus;
                if (craneActive != null)
                    myBoth.craneActie = craneActive;
                manager.SetMyBoth(myBoth);

                return myBoth;
            }
            catch
            {
                return null;
            }
        }


        
    }
}
