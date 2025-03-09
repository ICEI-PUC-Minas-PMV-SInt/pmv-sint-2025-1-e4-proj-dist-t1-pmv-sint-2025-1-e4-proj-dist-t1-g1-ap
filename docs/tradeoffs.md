# Trade-offs de Características de Qualidade

1. **Usabilidade**: 
   - A aplicação deverá ser responsiva, permitindo a visualização em celulares e outros dispositivos de forma adequada.
   - A aplicação deve ter bom nível de contraste entre os elementos da tela em conformidade.

1. **Desempenho**:
   - A aplicação deve usar serviço de object storage para armazenar arquivos estáticos.
   - A aplicação deve ser escalável para suportar quantidade massiva de acesso.

3. **Confiabilidade**:
   - A aplicação deve ser publicada em um ambiente acessível publicamente na Internet (Repl.it, GitHub Pages, Heroku)
     
4. **Suportabilidade**: 
   - A aplicação deve ser compatível com diversos dispositivos, sendo eles mobile (Android e iOS) ou desktop (Windows e macOS)
   - A aplicação deve ser compatível com os principais navegadores do mercado (Google Chrome, Firefox e Microsoft Edge)


A importância relativa de cada categoria:

| Categoria | Usabilidade | Desempenho | Confiabilidade | Suportabilidade |
| --- | --- | --- | --- | --- |
| Usabilidade | - | > | > | > |
| Desempenho | < | - | < | > |
| Confiabilidade | < | > | - | > |
| Suportabilidade | < | < | < | - |

> Nesta matriz, o sinal ">" indica que a categoria da linha é mais importante que a categoria da coluna, e o sinal "<" indica que a categoria da linha é menos importante que a categoria da coluna. Por exemplo, a Usabilidade é considerada mais importante que o Desempenho, Confiabilidade e Suportabilidade, enquanto o Desempenho é considerado mais importante que a Suportabilidade, mas menos importante que a Usabilidade e Confiabilidade, e assim por diante.

[Retorna](../README.md)
