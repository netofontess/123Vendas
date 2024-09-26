# 123Vendas

Rodar via Docker, exemplo:
```
docker build -t 123vendas-api .
docker run -d -p 8080:80 --name 123vendas-api-container 123vendas-api
```

Banco rodando em SQL Server, dados de conexão estão no AppSettings.
