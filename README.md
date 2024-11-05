# Projeto Cálculo de CDB

Este projeto implementa uma aplicação para cálculo de CDB, considerando alíquotas de impostos de acordo com prazos de investimento. A aplicação foi desenvolvida com .NET 7, seguindo princípios SOLID e uma arquitetura em serviços.

O projeto utiliza uma arquitetura de serviços, separando lógica de aplicação, domínio e infraestrutura. Abaixo estão os padrões e princípios aplicados:

- **Padrões de Projeto**: 
  - **Repository Pattern**: `CDBRepository` atua como um repositório simulado, calculando o CDB em memória sem persistência real.
  - **Injeção de Dependências**: Utilizado com `ServiceCollection` e `ServiceProvider` para facilitar testes e promover extensibilidade.

- **Princípios SOLID**:
  - **Responsabilidade Única (SRP)**: Cada classe executa uma função específica.
  - **Inversão de Dependência (DIP)**: Interfaces (`ICDBRepository`) desacoplam as implementações concretas, facilitando testes.
  - **Segregação de Interface (ISP)**: Interfaces contêm métodos específicos e necessários para cada camada.

## Configuração e Inicialização do Projeto

### Pré-requisitos

- .NET SDK 7.0 ou superior.

### Passo a Passo

1. Clone o repositório:
   ```bash
   git clone https://seu-repositorio-url
   cd seu-repositorio
dotnet restore
dotnet run --project src/B3.Application

Cálculo de CDB no Nível mais Baixo
O cálculo de CDB é realizado na camada de infraestrutura, na classe CDBRepository. Ele simula uma operação de banco de dados ao calcular o valor bruto e líquido com base nas taxas e prazos fornecidos:

Valor Bruto: Calculado aplicando-se uma taxa CDI e taxa bancária multiplicada a cada mês.
Valor Líquido: É aplicado um imposto de acordo com o prazo (6, 12 ou 24 meses), variando entre 22,5% e 15%.
Testes Unitários
Os testes unitários garantem que o cálculo do CDB seja preciso e que as regras de negócio sejam seguidas. A classe de testes CDBApplicationTests usa DependencyInjectionStartup para configurar os serviços e simular dependências com ICDBRepository.

Observação tomei a liberdade de configurar o swagger para facilitar os teste, não criei o fronte pois não é minha especialidade então foquei nos padrões e arquitetura.
deixei um arquivo docker configurado tambem. 
