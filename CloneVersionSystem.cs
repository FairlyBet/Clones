using System.Collections.Generic;
using System.Linq;


namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        private readonly List<CloneUnit> _clones;


        public CloneVersionSystem()
        {
            _clones = new List<CloneUnit>
            {
                new CloneUnit("1")
            };
        }

        public string Execute(string query)
        {
            var splittedQuery = query.Split();
            var command = splittedQuery.First();
            var cloneId = splittedQuery.Skip(1).First();
            switch (command)
            {
                case Operations.Learn:
                    var programId = splittedQuery.Skip(2).First();
                    Learn(cloneId, programId);
                    return null;
                case Operations.Rollback:
                    Rollback(cloneId);
                    return null;
                case Operations.Relearn:
                    Relearn(cloneId);
                    return null;
                case Operations.Clone:
                    Clone(cloneId);
                    return null;
                case Operations.Check:
                    return Check(cloneId);
                default:
                    return null;
            }
        }

        private void Learn(string cloneId, string programId)
        {
            var id = int.Parse(cloneId);
            if (id > _clones.Count)
            {
                var clone = new CloneUnit(cloneId);
                _clones.Add(clone);
            }
            _clones[id - 1].Learn(programId);
        }

        private void Rollback(string cloneId)
        {
            _clones[int.Parse(cloneId) - 1].Rollback();
        }

        private void Relearn(string cloneId)
        {
            _clones[int.Parse(cloneId) - 1].Relearn();
        }

        private void Clone(string cloneId)
        {
            var idOfNewClone = (int.Parse(_clones.Last().Id) + 1).ToString();
            var id = int.Parse(cloneId);
            var newClone = _clones[id - 1].Clone(idOfNewClone);
            _clones.Add(newClone);
        }

        private string Check(string cloneId)
        {
            return _clones[int.Parse(cloneId) - 1].Check();
        }
    }


    public class CloneUnit
    {
        public readonly string Id;
        private readonly LearningHistory _history;


        public CloneUnit(string id)
        {
            Id = id;
            _history = new LearningHistory();
        }

        public CloneUnit(string id, LearningHistory history)
        {
            Id = id;
            _history = new LearningHistory(history);
        }

        public void Learn(string programId)
        {
            _history.Learn(programId);
        }

        public void Rollback()
        {
            _history.Rollback();
        }

        public void Relearn()
        {
            _history.Relearn();
        }

        public CloneUnit Clone(string idOfNewCLone)
        {
            return new CloneUnit(idOfNewCLone, _history);
        }

        public string Check()
        {
            return _history.Check();
        }
    }


    public class LearningHistory
    {
        private Stack<string> _learnedPrograms;
        private Stack<string> _rolledBackPrograms;
        private bool _isShared;


        public LearningHistory()
        {
            _learnedPrograms = new Stack<string>();
            _rolledBackPrograms = new Stack<string>();
            _learnedPrograms.Push("basic");
        }

        public LearningHistory(LearningHistory history)
        {
            _learnedPrograms = history._learnedPrograms;
            _rolledBackPrograms = history._rolledBackPrograms;
            history._isShared = true;
            _isShared = true;
        }

        public void Learn(string programId)
        {
            CreateOwnCollections();
            _learnedPrograms.Push(programId);
            _rolledBackPrograms.Clear();
        }

        public void Rollback()
        {
            CreateOwnCollections();
            var program = _learnedPrograms.Pop();
            _rolledBackPrograms.Push(program);
        }

        public void Relearn()
        {
            CreateOwnCollections();
            var program = _rolledBackPrograms.Pop();
            _learnedPrograms.Push(program);
        }

        public string Check()
        {
            return _learnedPrograms.First();
        }

        private void CreateOwnCollections()
        {
            if (_isShared)
            {
                _learnedPrograms = new Stack<string>(_learnedPrograms.Reverse());
                _rolledBackPrograms = new Stack<string>(_rolledBackPrograms.Reverse());
                _isShared = false;
            }
        }
    }


    public static class Operations
    {
        public const string Learn = "learn";
        public const string Rollback = "rollback";
        public const string Relearn = "relearn";
        public const string Clone = "clone";
        public const string Check = "check";
    }
}
