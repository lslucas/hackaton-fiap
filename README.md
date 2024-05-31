# Consultas Médicas

Projeto de entrega final no hackaton do curso de Arquitetura de Sistemas .NET com Azure da Alura/FIAP.
Esse projeto é um inicio de site de uma clínica, onde teremos médicos cadastrados e os pacientes poderão acessar, abrir uma conta e agendar consultas. Com as consultas registradas, os pacientes serão notificados por email quando estiver próximo da data dessa consulta.

## Características Técnicas

- .NET7
- SQLServer da Azure
- Sistema de notificações smtp usando o servidor do google
- Dockerizado

## Instação
Executar o seguinte comando:

```sh
cd ConsultasMedicas
docker build -t clinica
docker run -p 3000:3000 clinica
```
