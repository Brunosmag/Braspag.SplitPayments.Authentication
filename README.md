**Qual a finalidade dessa biblioteca?**

A principal finalidade é  ilustrar para os desenvolvedores que utilizam/pretendem utilizar o serviço de Subadquirência/Split de Pagamentos como o processo de autenticação deve ser efetuado.
Demais informações sobre autenticação, modelos de negócio, fluxos transacionais podem ser encontradas em https://braspag.github.io//manual/split-de-pagamentos-cielo-e-commerce. 


**Padrão de autenticação.**

O fluxo de autenticação utiliza o padrão OAuth2. Ao se registrar para utilizar os serviços da Braspag você irá receber um ClientId/ClientSecret. Ambas credenciais devem ser utilizadas para gerar o Token de autenticação.

A validade do token é determinada através da propriedade "expires_in". É importante frisar que a validade nos ambientes de Produção/Sandbox é diferente.

**Como essa biblioteca funciona?**

A interface `IBraspagTokenOrchestrator` é disponibilizada com os métodos `CreateProductionTokenAsync` e `CreateSandboxTokenAsync`. Ambas recebem as informações de ClientId/ClientSecret e realizam a autenticação, retornando sempre um token válido.

As informações de ClientId/ClientSecret são sigilosas e é altamente recomendado que estejam armazenadas de maneira secreta e em ambiente seguro.

A biblioteca conta que cache em memoria, que irá proporcionar mais agilidade no processo de geração do token, evitando chamadas desnecessárias para o serviço de autenticação (conhecido como BraspagAuth). Dessa forma, o consumidor não precisa se preocupar com a validação do "expires_in" que é retornado pelo serviço de autenticação.

**Como utilizar?**

Como o desenvolvimento foi feito utilizando .NET Standard 2.1, todas as demais implementações do .NET compatíveis com essa versão terão sucesso na utilização. A documentação https://docs.microsoft.com/pt-br/dotnet/standard/net-standard estabelece a relação entre a implementação do .NET Standard e suas compatibilidades com demais versões do .NET.
