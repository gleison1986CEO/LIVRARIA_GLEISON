- curl -sSL https://ngrok-agent.s3.amazonaws.com/ngrok.asc \
  | sudo tee /etc/apt/trusted.gpg.d/ngrok.asc >/dev/null \
  && echo "deb https://ngrok-agent.s3.amazonaws.com buster main" \
  | sudo tee /etc/apt/sources.list.d/ngrok.list \
  && sudo apt update \
  && sudo apt install ngrok
  
- ngrok config add-authtoken 2fxYNPA6iqs8OobbFJw0Nd2XOqb_3YuDDQ5SvuuW7CqXwkhU6

- ngrok http --url=wittadv.ngrok.app 7133
- ngrok http --url=apiwitt.ngrok.app 7128
- ngrok http --url=laudos.ngrok.app 80

