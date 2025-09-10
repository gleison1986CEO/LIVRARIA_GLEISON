namespace sistema.Classes
{
    public class Error
    {
        public String errorSearch()
        {
            ///ERROR WHEN SEARCH
            var errorString =  "Desculpe, não encontramos nenhum resultado correspondente à sua busca. Por favor, verifique se os termos de pesquisa estão corretos ou tente utilizar filtros diferentes. Se precisar de ajuda, não hesite em nos contatar para obter suporte.";
            return errorString;
        }

         public String errorSystem()
        {
            ///COLLECTION DATA
            var errorString = "dados inconsistentes no sistemas, porfavor tente novamente mais tarde!"; 
            return errorString;
        }

    }
}
