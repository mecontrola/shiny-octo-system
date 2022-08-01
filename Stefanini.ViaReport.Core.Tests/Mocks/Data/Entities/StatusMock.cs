﻿using Stefanini.ViaReport.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Stefanini.ViaReport.Core.Tests.Mocks.Data.Entities
{
    public class StatusMock
    {
        public static Status CreateBacklog()
            => new()
            {
                Key = DataMock.INT_STATUS_BACKLOG,
                Name = DataMock.ISSUE_STATUS_1
            };

        public static Status CreateRefinamentoFuncional()
            => new()
            {
                Key = DataMock.INT_STATUS_REFINAMENTO_FUNCIONAL,
                Name = DataMock.TEXT_STATUS_REFINAMENTO_FUNCIONAL
            };

        public static Status CreateRefinamentoTecnico()
            => new()
            {
                Key = DataMock.INT_STATUS_REFINAMENTO_TECNICO,
                Name = DataMock.TEXT_STATUS_REFINAMENTO_TECNICO
            };

        public static Status CreateReplanishment()
            => new()
            {
                Key = DataMock.INT_STATUS_REPLENISHMENT,
                Name = DataMock.ISSUE_STATUS_2
            };

        public static Status CreateParaDesenvolvimento()
            => new()
            {
                Key = DataMock.INT_STATUS_PARA_DESENVOLVIMENTO,
                Name = DataMock.TEXT_STATUS_PARA_DESENVOLVIMENTO
            };

        public static Status CreateEmDesenvolvimento()
            => new()
            {
                Key = DataMock.INT_STATUS_EM_DESENVOLVIMENTO,
                Name = DataMock.TEXT_STATUS_EM_DESENVOLVIMENTO
            };

        public static Status CreateParaRevisao()
            => new()
            {
                Key = DataMock.INT_STATUS_PARA_REVISAO,
                Name = DataMock.TEXT_STATUS_PARA_REVISAO
            };

        public static Status CreateEmRevisao()
            => new()
            {
                Key = DataMock.INT_STATUS_EM_REVISAO,
                Name = DataMock.TEXT_STATUS_EM_REVISAO
            };

        public static Status CreateParaTeste()
            => new()
            {
                Key = DataMock.INT_STATUS_PARA_TESTE,
                Name = DataMock.TEXT_STATUS_PARA_TESTE
            };

        public static Status CreateEmTeste()
            => new()
            {
                Key = DataMock.INT_STATUS_EM_TESTE,
                Name = DataMock.TEXT_STATUS_EM_TESTE
            };

        public static Status CreateParaHomologacao()
            => new()
            {
                Key = DataMock.INT_STATUS_PARA_HOMOLOGACAO,
                Name = DataMock.TEXT_STATUS_PARA_HOMOLOGACAO
            };

        public static Status CreateEmHomologacao()
            => new()
            {
                Key = DataMock.INT_STATUS_EM_HOMOLOGACAO,
                Name = DataMock.TEXT_STATUS_EM_HOMOLOGACAO
            };

        public static Status CreateDone()
            => new()
            {
                Key = DataMock.INT_STATUS_DONE,
                Name = DataMock.ISSUE_STATUS_2,
            };

        public static Status CreateRemoved()
            => new()
            {
                Key = DataMock.INT_STATUS_REMOVED,
                Name = DataMock.TEXT_STATUS_REMOVED,
            };

        public static Status CreateCancelled()
            => new()
            {
                Key = DataMock.INT_STATUS_CANCELLED,
                Name = DataMock.TEXT_STATUS_CANCELLED,
            };

        public static Status[] CreateListToDo()
            => new[]
            {
                CreateBacklog(),
                CreateRefinamentoFuncional(),
                CreateRefinamentoTecnico(),
                CreateReplanishment()
            };

        public static Status[] CreateListInProgress()
            => new[]
            {
                CreateParaDesenvolvimento(),
                CreateEmDesenvolvimento(),
                CreateParaRevisao(),
                CreateEmRevisao(),
                CreateParaTeste(),
                CreateEmTeste(),
                CreateParaHomologacao(),
                CreateEmHomologacao(),
            };

        public static Status[] CreateListDone()
            => new[]
            {
                        CreateDone(),
                        CreateRemoved(),
                        CreateCancelled()
            };

        public static Status[] CreateListAll()
            => CreateListToDo().Concat(CreateListInProgress())
                               .Concat(CreateListDone())
                               .ToArray();

        public static IDictionary<string, long> CreateDictionary()
            => new Dictionary<string, long>
            {
                { DataMock.INT_STATUS_BACKLOG.ToString(), DataMock.INT_ID_1 },
                { DataMock.INT_STATUS_REFINAMENTO_FUNCIONAL.ToString(), DataMock.INT_ID_2 },
                { DataMock.INT_STATUS_REFINAMENTO_TECNICO.ToString(), DataMock.INT_ID_3 },
                { DataMock.INT_STATUS_REPLENISHMENT.ToString(), DataMock.INT_ID_4 },
                { DataMock.INT_STATUS_PARA_DESENVOLVIMENTO.ToString(), DataMock.INT_ID_5 },
                { DataMock.INT_STATUS_EM_DESENVOLVIMENTO.ToString(), DataMock.INT_ID_6 },
                { DataMock.INT_STATUS_PARA_REVISAO.ToString(), 7 },
                { DataMock.INT_STATUS_EM_REVISAO.ToString(), 8 },
                { DataMock.INT_STATUS_TO_TEST.ToString(), 9 },
                { DataMock.INT_STATUS_IN_TEST.ToString(), 10 },
                { DataMock.INT_STATUS_PARA_HOMOLOGACAO.ToString(), 11 },
                { DataMock.INT_STATUS_EM_HOMOLOGACAO.ToString(), 12 },
                { DataMock.INT_STATUS_DONE.ToString(), 13 },
            };
    }
}