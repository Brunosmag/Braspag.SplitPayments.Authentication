# Braspag.SplitPayments.Authentication

![GitHub repo size](https://img.shields.io/github/repo-size/Brunosmag/braspag-authentication?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/Brunosmag/braspag-authentication?style=for-the-badge)

> A principal finalidade dessa biblioteca é  ilustrar para os desenvolvedores que utilizam/pretendem utilizar o serviço de Subadquirência/Split de Pagamentos da Braspag como o processo de autenticação deve ser efetuado.
Demais informações sobre autenticação, modelos de negócio, fluxos transacionais podem ser encontradas em https://braspag.github.io//manual/split-de-pagamentos-cielo-e-commerce. 


## 💻 Pré-requisitos

* Seu projeto utiliza uma versão do .NET compatível com .NET Standard 2.1.
* Você possui um ClientId/ClientSecret apto a utilizar os serviços de Split de Pagamentos (Marketplace) da Braspag.

## 🚀 Instalando <Braspag.SplitPayments.Authentication>

Nuget:
```
Install-Package Braspag.SplitPayments.Authentication -Version 1.0.0
```

## ☕ Usando <Braspag.SplitPayments.Authentication>

A interface `IBraspagTokenOrchestrator` é disponibilizada com os métodos `CreateProductionTokenAsync` e `CreateSandboxTokenAsync`. Ambas recebem as informações de ClientId/ClientSecret e realizam a autenticação, retornando sempre um token válido.

As informações de ClientId/ClientSecret são sigilosas e é altamente recomendado que estejam armazenadas de maneira secreta e em ambiente seguro.

A biblioteca conta que cache em memoria, que irá proporcionar mais agilidade no processo de geração do token, evitando chamadas desnecessárias para o serviço de autenticação (conhecido como BraspagAuth). Dessa forma, o consumidor não precisa se preocupar com a validação do "expires_in" que é retornado pelo serviço de autenticação.

Tenha certeza que você registrou os seguintes serviços no seu container de injeção de dependência:

```
services.AddBraspagAuthentication();
services.AddMemoryCache();
services.AddHttpClient();
```

## 📫 Contribuindo para <Braspag.SplitPayments.Authentication>
Para contribuir com <Braspag.SplitPayments.Authentication>, siga estas etapas:

1. Bifurque este repositório.
2. Crie um branch: `git checkout -b <nome_branch>`.
3. Faça suas alterações e confirme-as: `git commit -m '<mensagem_commit>'`
4. Envie para o branch original: `git push origin <nome_do_projeto> / <local>`
5. Crie a solicitação de pull.

Como alternativa, consulte a documentação do GitHub em [como criar uma solicitação pull](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request).

Vale ressaltar que essa biblioteca é totalmente open-source, não possui nenhum vínculo direto com a empresa Braspag e está em constante desenvolvimento e aprimoramento.

