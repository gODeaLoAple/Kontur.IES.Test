using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ArchiveTask
{
    public class Archive
    {
        private readonly List<ArchivedOperation> _operations;

        public Archive(string[] serializedOperations)
        {
            _operations = serializedOperations
                .Select(x => JsonSerializer.Deserialize<ArchivedOperation>(x))
                .ToList();
        }

        public Guid[] GetOperationIds(string time)
        {
            var date = DateTime.Parse(time);
            return _operations
                .Where(x => x.Time == date)
                .Select(x => x.OperationId)
                .ToArray();
        }

        private class ArchivedOperation
        {
            public Guid OperationId { get; set; }
            public DateTime Time { get; set; }
        }
    }
}