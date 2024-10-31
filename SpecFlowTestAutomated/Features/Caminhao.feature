Feature: Caminhao
  Funcionalidade para gerenciar caminhões na coleta de lixo

  Scenario: Consultar caminhão por ID existente
    Given que existe um caminhão com o ID "1"
    When o usuário requisita o caminhão pelo ID "1"
    Then o resultado deve ser o caminhão correspondente
    And o status da resposta deve ser "200 OK"

  Scenario: Consultar caminhão por ID inexistente
    Given que não existe caminhão com o ID "999"
    When o usuário requisita o caminhão pelo ID "999"
    Then o resultado deve ser uma resposta "Not Found"
    And o status da resposta deve ser "404 Not Found"

  Scenario: Criar novo caminhão com sucesso
    Given um novo caminhão com dados válidos
    When o usuário cria um novo caminhão
    Then o resultado deve ser o caminhão criado
    And o status da resposta deve ser "201 Created"

  Scenario: Tentar criar caminhão com dados inválidos
    Given um novo caminhão com dados inválidos
    When o usuário tenta criar o caminhão
    Then o resultado deve ser uma resposta "Bad Request"
    And o status da resposta deve ser "400 Bad Request"

  Scenario: Atualizar caminhão existente
    Given que existe um caminhão com o ID "1"
    And o usuário possui dados válidos para atualização
    When o usuário atualiza o caminhão com o ID "1"
    Then o resultado deve ser uma resposta "No Content"
    And o status da resposta deve ser "204 No Content"

  Scenario: Excluir caminhão existente
    Given que existe um caminhão com o ID "1"
    When o usuário exclui o caminhão com o ID "1"
    Then o resultado deve ser uma resposta "No Content"
    And o status da resposta deve ser "204 No Content"

  Scenario: Excluir caminhão inexistente
    Given que não existe caminhão com o ID "999"
    When o usuário tenta excluir o caminhão com o ID "999"
    Then o resultado deve ser uma resposta "Not Found"
    And o status da resposta deve ser "404 Not Found"
