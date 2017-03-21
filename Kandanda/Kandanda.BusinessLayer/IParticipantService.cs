using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer
{
    interface IParticipantService
    {
        List<Participant> GetAll();
    }
}
