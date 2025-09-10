# START SERVICE

- pm2 start "flask run"  --name "SERVICE" --exp-backoff-restart-delay=100
- pm2 start "python3 app.py"  --name "SERVICE-USERS" --exp-backoff-restart-delay=100
- pm2 start "dotnet run" --name "SISTEMA" --exp-backoff-restart-delay=100
- pm2 start "dotnet run" --name "api" --exp-backoff-restart-delay=100
- pm2 start "npm start" --name "backend" --exp-backoff-restart-delay=100
- pm2 start "npm start" --name "frontend" --exp-backoff-restart-delay=100


- pm2 start "ngrok http --url=wittadv.ngrok.app 7133" --name "witt_sistema" --exp-backoff-restart-delay=100 && pm2 start "ngrok http --url=apiwitt.ngrok.app 7128" --name "witt_api" --exp-backoff-restart-delay=100 && pm2 start "ngrok http --url=laudos.ngrok.app 80" --name "witt_laudos" --exp-backoff-restart-delay=100 && pm2 start "ngrok http --url=spiderx.ngrok.app 3000" --name "witt_spiderx" --exp-backoff-restart-delay=100 && pm2 start "ngrok http --url=spiderxapi.ngrok.app 4000" --name "witt_spiderx_api" --exp-backoff-restart-delay=100


- pm2 save
- pm2 stop all
- pm2 start all

## TESTE API CALLBACK
- pm2 start "ngrok tcp 5000 --url tcp://3.tcp.ngrok.io:27198" --name "POSTGRESS" --exp-backoff-restart-delay=100

## I.A.
- ollama run deepseek-r1:14b




# START SERVICE

- CHANGE URLS
- VERIFY ALL

- pm2 start "ngrok http --url=sistema.ngrok.dev 7133" --name "SISTEMA" --exp-backoff-restart-delay=100

- pm2 start "ngrok http --url=files.ngrok.dev 80" --name "ARQUIVOS" --exp-backoff-restart-delay=100


- pm2 save
- pm2 stop all
- pm2 start all



# REGRA
## PARCEIRO
- PAGAMENTO PRIVADO
- CADASTRA DNS
- CADSASTR HOSTNAME
- LIMITE DE CADASTROS

## REVENDA
- PAGAMENTO VIA MERCADO PAGO
- COMPRA DE CREDITOS
- REPASSA O DNS E HOSTNAME CADASTRO PELO PARCEIRO
- CREDITO E REMOVIDO CONFORME O LOGIN É FEITO PELO CLIENTE NO APP
## SUPER ADMIN 
- VE TODAS FUNÇÕES
- FAZ TODAS AS FUNÇÇÕES