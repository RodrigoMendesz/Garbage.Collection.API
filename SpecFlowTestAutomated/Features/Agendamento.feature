Feature: AgendamentoController
  Controller para agendamento de coletas de lixo inteligente

  @getById
  Scenario: Consultar agendamento por ID com sucesso
    Given que existe um agendamento com o ID "1"
    When o usuário requisita o agendamento pelo ID "1"
    Then o resultado deve ser o agendamento correspondente
    And o status da resposta deve ser "200 OK"

  @getByIdNotFound
  Scenario: Consultar agendamento com ID inexistente
    Given que não existe agendamento com o ID "999"
    When o usuário requisita o agendamento pelo ID "999"
    Then o resultado deve ser uma resposta "Not Found"
    And o status da resposta deve ser "404 Not Found"

  @createAgendamento
  Scenario: Criar novo agendamento com sucesso
    Given um novo agendamento com dados válidos
    When o usuário cria um novo agendamento
    Then o resultado deve ser o agendamento criado
    And o status da resposta deve ser "201 Created"

  @createAgendamentoInvalid
  Scenario: Tentar criar agendamento com dados inválidos
    Given um novo agendamento com dados inválidos
    When o usuário tenta criar o agendamento
    Then o resultado deve ser uma resposta "Bad Request"
    And o status da resposta deve ser "400 Bad Request"

  @updateAgendamento
  Scenario: Atualizar agendamento existente
    Given que existe um agendamento com o ID "1"
    And o usuário possui dados válidos para atualização
    When o usuário atualiza o agendamento com o ID "1"
    Then o resultado deve ser uma resposta "No Content"
    And o status da resposta deve ser "204 No Content"

  @updateAgendamentoInvalidId
  Scenario: Tentar atualizar agendamento com ID inválido
    Given que o ID "999" não existe
    When o usuário tenta atualizar o agendamento com o ID "999"
    Then o resultado deve ser uma resposta "Not Found"
    And o status da resposta deve ser "404 Not Found"

  @deleteAgendamento
  Scenario: Excluir agendamento existente
    Given que existe um agendamento com o ID "1"
    When o usuário exclui o agendamento com o ID "1"
    Then o resultado deve ser uma resposta "No Content"
    And o status da resposta deve ser "204 No Content"

  @deleteAgendamentoInvalidId
  Scenario: Tentar excluir agendamento com ID inexistente
    Given que o ID "999" não existe
    When o usuário tenta excluir o agendamento com o ID "999"
    Then o resultado deve ser uma resposta "Not Found"
    And o status da resposta deve ser "404 Not Found"
