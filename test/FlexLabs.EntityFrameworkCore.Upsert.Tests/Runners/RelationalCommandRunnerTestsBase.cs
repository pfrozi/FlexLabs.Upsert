﻿using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using FlexLabs.EntityFrameworkCore.Upsert.Internal;
using FlexLabs.EntityFrameworkCore.Upsert.Runners;
using Xunit;

namespace FlexLabs.EntityFrameworkCore.Upsert.Tests.Runners
{
    public abstract class RelationalCommandRunnerTestsBase
    {
        protected abstract RelationalUpsertCommandRunner GetRunner();

        protected abstract string NoUpdate_Sql { get; }
        [Fact]
        public void SqlSyntaxRunner_NoUpdate()
        {
            var runner = GetRunner();
            var tableName = "myTable";
            ICollection<(string ColumnName, ConstantValue Value)> entity = new[]
            {
                ( "Name", new ConstantValue("value") { ArgumentIndex = 0 } ),
                ( "Status", new ConstantValue("status") { ArgumentIndex = 1} ),
            };

            //TODO:
            var generatedSql = runner.GenerateCommand(tableName, IsolationLevel.ReadCommitted, new[] { entity }, new[] { ("ID", false) }, null);

            Assert.Equal(NoUpdate_Sql, generatedSql);
        }

        protected abstract string NoUpdate_WithNullable_Sql { get; }
        [Fact]
        public void SqlSyntaxRunner_NoUpdate_WithNullable()
        {
            var runner = GetRunner();
            var tableName = "myTable";
            ICollection<(string ColumnName, ConstantValue Value)> entity = new[]
            {
                ( "Name", new ConstantValue("value") { ArgumentIndex = 0 } ),
                ( "Status", new ConstantValue("status") { ArgumentIndex = 1} ),
            };

            //TODO:
            var generatedSql = runner.GenerateCommand(tableName, IsolationLevel.ReadCommitted, new[] { entity }, new[] { ("ID1", false), ("ID2", true) }, null);

            Assert.Equal(NoUpdate_WithNullable_Sql, generatedSql);
        }

        protected abstract string Update_Constant_Sql { get; }
        [Fact]
        public void SqlSyntaxRunner_Update_Constant()
        {
            var runner = GetRunner();
            var tableName = "myTable";
            ICollection<(string ColumnName, ConstantValue Value)> entity = new[]
            {
                ( "Name", new ConstantValue("value") { ArgumentIndex = 0 } ),
                ( "Status", new ConstantValue("status") { ArgumentIndex = 1} ),
            };
            var updates = new[]
            {
                ("Name", new KnownExpression(ExpressionType.Constant, new ConstantValue("newValue") { ArgumentIndex = 2 }))
            };

            //TODO:
            var generatedSql = runner.GenerateCommand(tableName, IsolationLevel.ReadCommitted, new[] { entity }, new[] { ("ID", false) }, updates);

            Assert.Equal(Update_Constant_Sql, generatedSql);
        }

        protected abstract string Update_Source_Sql { get; }
        [Fact]
        public void SqlSyntaxRunner_Update_Source()
        {
            var runner = GetRunner();
            var tableName = "myTable";
            ICollection<(string ColumnName, ConstantValue Value)> entity = new[]
            {
                ( "Name", new ConstantValue("value") { ArgumentIndex = 0 } ),
                ( "Status", new ConstantValue("status") { ArgumentIndex = 1} ),
            };
            var updates = new[]
            {
                ("Name", new KnownExpression(ExpressionType.MemberAccess, new ParameterProperty("Name", false) { Property = new MockProperty("Name") }))
            };

            //TODO:
            var generatedSql = runner.GenerateCommand(tableName, IsolationLevel.ReadCommitted, new[] { entity }, new[] { ("ID", false) }, updates);

            Assert.Equal(Update_Source_Sql, generatedSql);
        }

        protected abstract string Update_Source_RenamedCol_Sql { get; }
        [Fact]
        public void SqlSyntaxRunner_Update_Source_RenamedCol()
        {
            var runner = GetRunner();
            var tableName = "myTable";
            ICollection<(string ColumnName, ConstantValue Value)> entity = new[]
            {
                ( "Name", new ConstantValue("value") { ArgumentIndex = 0 } ),
                ( "Status", new ConstantValue("status") { ArgumentIndex = 1} ),
            };
            var updates = new[]
            {
                ("Name", new KnownExpression(ExpressionType.MemberAccess, new ParameterProperty("Name", false) { Property = new MockProperty("Name2") }))
            };

            //TODO:
            var generatedSql = runner.GenerateCommand(tableName, IsolationLevel.ReadCommitted, new[] { entity }, new[] { ("ID", false) }, updates);

            Assert.Equal(Update_Source_RenamedCol_Sql, generatedSql);
        }

        protected abstract string Update_BinaryAdd_Sql { get; }
        [Fact]
        public void SqlSyntaxRunner_Update_BinaryAdd()
        {
            var runner = GetRunner();
            var tableName = "myTable";
            ICollection<(string ColumnName, ConstantValue Value)> entity = new[]
            {
                ( "Name", new ConstantValue("value") { ArgumentIndex = 0 } ),
                ( "Status", new ConstantValue(3) { ArgumentIndex = 1} ),
            };
            var updates = new[]
            {
                ("Status", new KnownExpression(ExpressionType.Add,
                    new ParameterProperty("Status", true) { Property = new MockProperty("Status") },
                    new ConstantValue(1) { ArgumentIndex = 2 }))
            };

            //TODO:
            var generatedSql = runner.GenerateCommand(tableName, IsolationLevel.ReadCommitted, new[] { entity }, new[] { ("ID", false) }, updates);

            Assert.Equal(Update_BinaryAdd_Sql, generatedSql);
        }

        protected abstract string Update_Coalesce_Sql { get; }
        [Fact]
        public void SqlSyntaxRunner_Update_Coalesce()
        {
            var runner = GetRunner();
            var tableName = "myTable";
            ICollection<(string ColumnName, ConstantValue Value)> entity = new[]
            {
                ( "Name", new ConstantValue("value") { ArgumentIndex = 0 } ),
                ( "Status", new ConstantValue(3) { ArgumentIndex = 1} ),
            };
            var updates = new[]
            {
                ("Status", new KnownExpression(ExpressionType.Coalesce,
                    new ParameterProperty("Status", true) { Property = new MockProperty("Status") },
                    new ConstantValue(1) { ArgumentIndex = 2 }))
            };

            //TODO:
            var generatedSql = runner.GenerateCommand(tableName, IsolationLevel.ReadCommitted, new[] { entity }, new[] { ("ID", false) }, updates);

            Assert.Equal(Update_Coalesce_Sql, generatedSql);
        }
    }
}
