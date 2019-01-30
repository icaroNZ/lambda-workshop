using HelloLambda.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloLambda
{
    public class MyInsertUseCase
    {
        private readonly IOracleRepository _oracleRepository;
        public MyInsertUseCase(IOracleRepository oracleRepository)
        {
            _oracleRepository = oracleRepository;
        }
        public void InsertWorkshopMessage(string message)
        {
            MyUseCaseCommon.ValidateMessage(message);
            _oracleRepository.InsertRecord(message);
        }
    }
}
