using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Data;
using AutoMapper;
using Payroll.Entities;
using Payroll.Data.Repositories;
using Payroll.Data.Infrastructure;
using Payroll.WebApp.Models;
using Payroll.WebApp.Infrastructure;
using Payroll.WebApp.Infrastructure.Extensions;
using System.Data.Entity.Infrastructure;
using Payroll.WebApp.Infrastructure.Core;


/// <summary>
/// *******************NOT USED HAS BUG
/// </summary>

namespace Payroll.WebApp.BushinessProcesses
{
    public class TimeCardManage : BushinessProcessesMain
    {

        //     PayrollContext pr = new PayrollContext();

        private readonly IEntityBaseRepository<TimeCard> _timecardRepository;


        // TimeCardViewModel timecardVM;

        public TimeCardManage(IEntityBaseRepository<TimeCard> timecardRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
           : base(_errorsRepository, _unitOfWork)
        {
            _timecardRepository = timecardRepository;

        }

        public string AddTimeCard()
        {
            TimeCard newtimecard = new TimeCard();

            TimeCardViewModel timecardVM = new TimeCardViewModel()
            { WorkedHours = 0 };
            try
            {
                newtimecard.UpdateTimeCard(timecardVM);

                //{ WorkedHours=0};

                //pr.TimeCard.Add(newTimeCard);
                //pr.Commit();           
                //  
                _timecardRepository.Add(newtimecard);
                _unitOfWork.Commit();

                return "New TimeCard added";
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.Message;
            }

            catch (Exception ex)
            {
                LogError(ex);
                return "No Timecard added" + ex.ToString();
            }
        }

        public int SelectMaxTimeCardId()
        {

            int outtimecard = 0;
            var maxtimecard = _timecardRepository.GetAll();

            outtimecard  = maxtimecard.Max(p => p.ID);
            return outtimecard;
        }


        /// <summary>
        /// Deleye Timecard
        /// </summary>
        /// <param name="CardId"></param>
        public void DeleteTimeCardById(TimeCard timecard)
        {
            _timecardRepository.Delete(timecard);
        }

    } 
}
