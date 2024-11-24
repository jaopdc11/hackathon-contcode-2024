// Fun��o para verificar se o usu�rio est� logado
function checkLogin() {
    // Verificar se o usu�rio j� est� autenticado
    const userLogged = document.cookie.split('; ').find(row => row.startsWith('userLogged='));

    if (!userLogged) {
        // Se o usu�rio n�o estiver logado, redirecionar para a p�gina de login
        if (window.location.pathname !== '/Home/Loggin') { // Verificar se j� n�o est� na p�gina de login
            window.location.href = '/Home/Loggin'; // Redireciona para a p�gina de login
        }
    } else {
        // Caso o usu�rio esteja logado, redirecionar para a p�gina principal (dashboard ou p�gina inicial)
        if (window.location.pathname === '/Home/Loggin') { // Verificar se est� na p�gina de login
            window.location.href = '/Home/Dashboard'; // Redireciona para a p�gina principal ap�s login
        }
    }
}

// Executar a verifica��o quando a p�gina for carregada
document.addEventListener('DOMContentLoaded', () => {
    checkLogin();
}); 
