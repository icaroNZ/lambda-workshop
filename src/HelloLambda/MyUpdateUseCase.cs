using HelloLambda.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloLambda
{
    public class MyUpdateUseCase
    {
        private readonly IOracleRepository _oracleRepository;
        public MyUpdateUseCase(IOracleRepository oracleRepository)
        {
            _oracleRepository = oracleRepository;
        }
        public void UpdateWorkshopMessage(int id, string message)
        {
            MyUseCaseCommon.ValidateId(id);
            MyUseCaseCommon.ValidateMessage(message);
            _oracleRepository.UpdateRecord(id, message);
        }

        
    }
}
