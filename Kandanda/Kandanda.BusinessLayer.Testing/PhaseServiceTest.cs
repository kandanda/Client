using System;
using Effort;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public sealed class PhaseServiceTest
    {
        private IPhaseService _phaseService;

        [TestInitialize]
        public void Setup()
        {
            _phaseService = new PhaseService(new KandandaDbContext(DbConnectionFactory.CreateTransient()));
        }

        [TestMethod]
        public void TestCreateEmpty()
        {
            const string PhaseName = "Gruppenphase";
            var from = new DateTime(2014, 1, 1);
            var until = new DateTime(2015, 1, 1);

            var phase = _phaseService.CreateEmpty();
            var reloadedPhase = _phaseService.GetPhaseById(phase.Id);

            Assert.IsNotNull(reloadedPhase);

            phase.Name = PhaseName;
            phase.From = from;
            phase.Until = new DateTime(2015, 1, 1);
            
            _phaseService.Update(phase);

            reloadedPhase = _phaseService.GetPhaseById(phase.Id);

            Assert.AreEqual(PhaseName, reloadedPhase.Name);
            Assert.AreEqual(from, reloadedPhase.From);
            Assert.AreEqual(until, reloadedPhase.Until);
        }

        [TestMethod]
        public void TestUpdatePhase()
        {
            var phase = _phaseService.CreateEmpty();
            Assert.IsNotNull(phase);

            phase.Name = "Fisch";
            _phaseService.Update(phase);

            var reloadedPhase = _phaseService.GetPhaseById(phase.Id);

            Assert.AreEqual(phase.Name, reloadedPhase.Name);
        }

        [TestMethod]
        public void TestGetAllPhases()
        {
            var phaseCount = _phaseService.GetAllPhases().Count;

            _phaseService.CreateEmpty();
            _phaseService.CreateEmpty();
            _phaseService.CreateEmpty();

            var newPhaseCount = _phaseService.GetAllPhases().Count;

            Assert.AreEqual(phaseCount + 3, newPhaseCount);
        }
    }
}
