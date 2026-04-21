# AquaMonitor - Solução ESG Inteligente

Este projeto faz parte do ecossistema de cidades inteligentes, focado no monitoramento e gestão eficiente de recursos hídricos. A aplicação consiste em uma API desenvolvida em .NET 10, containerizada com Docker e integrada a um pipeline de CI/CD automatizado via GitHub Actions.

# Como executar localmente (Docker)

Para subir o ambiente completo (API + Banco de Dados Oracle), siga os passos abaixo:

1.  **Pré-requisitos:** Certifique-se de ter o [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado.
2.  **Configuração de Ambiente:** * Renomeie o arquivo `.env.example` para `.env`.
    * Preencha as credenciais do banco de dados no arquivo `.env`.
3.  **Execução:** Na raiz do projeto, execute o comando:
    ```bash
    docker-compose up --build
    ```
4.  **Acesso:** Após a inicialização, a API estará disponível em:
    * Swagger (Documentação): `http://localhost:8080/swagger`

---

# Containerização

A estratégia de containerização foi desenhada para garantir portabilidade e segurança:

* **Dockerfile:** Utiliza **Multi-stage Build**, separando o ambiente de compilação (SDK 10.0) do ambiente de execução (Runtime 10.0), resultando em uma imagem leve e otimizada.
* **Segurança:** A aplicação foi configurada para rodar na porta **8080**, respeitando as diretrizes de execução não-root das imagens modernas do .NET.
* **Orquestração:** O `docker-compose.yml` gerencia a API e o banco de dados **Oracle XE**, utilizando `healthcheck` para garantir que a API só inicie após o banco estar pronto para conexões.

---

# Pipeline CI/CD

O fluxo de integração e entrega contínua foi implementado via **GitHub Actions** (`.github/workflows/main.yml`):

1.  **Build & Test:** Restaura dependências, compila o código em modo Release e executa os testes unitários.
2.  **Containerization:** Realiza o build da imagem Docker para validar o arquivo de receita.
3.  **Deploy Stages:** Simulação de deploy automatizado nos ambientes de **Staging** e **Production** após o sucesso das etapas anteriores.

---

# Tecnologias Utilizadas

* **Linguagem:** C# (.NET 10)
* **Banco de Dados:** Oracle Database 21c (Dockerized)
* **CI/CD:** GitHub Actions
* **Orquestração:** Docker Compose
* **Documentação API:** Swagger / OpenAPI

---

# Autor
* **Leandro Antonio da Silva** 

---

## Funcionamento e prints do projeto
* **Repositório:** [https://github.com/proleander/AquaMonitor-DevOps](https://github.com/proleander/AquaMonitor-DevOps)
* **Pipeline Status:**  Passed (Conforme documentação em PDF)