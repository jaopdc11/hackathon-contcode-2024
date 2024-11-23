## Estrutura do Banco de Dados

As principais tabelas modeladas até o momento são:

1. **Usuarios**  
   Armazena as informações dos funcionários.  
   Campos:  
   - `id`: Identificador único do usuário (PK).  
   - `nome`: Nome do usuário.

2. **Cargas**  
   Armazena os dados sobre as cargas.  
   Campos:  
   - `id`: Identificador único da carga (PK).  
   - `carga`: Nome ou descrição da carga.  
   - `categoria`: Categoria da carga.  
   - `quantidade`: Quantidade da carga.  
   - `peso`: Peso da carga.

3. **Caminhoes**  
   Informações sobre os caminhões.  
   Campos:  
   - `id`: Identificador único do caminhão (PK).  
   - `placa`: Placa do caminhão.  
   - `cargaId`: Referência à carga transportada (FK).  
   - `status`: Status do caminhão (aguardando, em trânsito, etc.).

4. **Operacoes**  
   Registra as operações realizadas, como entradas e saídas de cargas.  
   Campos:  
   - `id`: Identificador único da operação (PK).  
   - `caminhaoId`: Referência ao caminhão envolvido na operação (FK).  
   - `cargaId`: Referência à carga associada (FK).  
   - `usuarioId`: Referência ao usuário que registrou a operação (FK).  
   - `tipoOperacao`: Tipo da operação (entrada ou saída).  
   - `dataHoraOperacao`: Data e hora da operação.

5. **Vagas**  
   Representa as vagas do depósito.  
   Campos:  
   - `id`: Identificador único da vaga (PK).  
   - `localizacao`: Localização da vaga no depósito (ex: A1, B2, etc.).  
   - `cargaId`: Referência à carga armazenada na vaga (FK, pode ser NULL se a vaga estiver livre).  
   - `status`: Status da vaga (livre, ocupada, etc.).

6. **Estoque**  
   Relaciona as cargas com as vagas onde estão armazenadas.  
   Campos:  
   - `id`: Identificador único do registro de estoque (PK).  
   - `cargaId`: Referência à carga no estoque (FK, pode ser NULL se não houver carga).  
   - `quantidade`: Quantidade de carga no estoque.  
   - `vagaId`: Referência à vaga no depósito onde a carga está armazenada (FK).
