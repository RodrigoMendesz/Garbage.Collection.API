Feature: Endereco

  Scenario: Get Endereco by Id
    Given um Endereco existente com o id 1
    When o usuário solicita o Endereco com o id 1
    Then o resultado deve ser o Endereco correspondente

  Scenario: Create a new Endereco
    When o usuário cria um novo Endereco
    Then o Endereco deve ser criado com sucesso

  Scenario: Update an existing Endereco
    Given um Endereco existente com o id 1
    When o usuário atualiza o Endereco com o id 1
    Then o Endereco deve ser atualizado com sucesso

  Scenario: Delete an existing Endereco
    Given um Endereco existente com o id 1
    When o usuário solicita a exclusão do Endereco com o id 1
    Then o Endereco deve ser excluído com sucesso
