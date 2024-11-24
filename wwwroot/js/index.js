// Função para verificar se o usuário está logado
function checkLogin() {
    // Verificar se o usuário já está autenticado
    const userLogged = document.cookie.split('; ').find(row => row.startsWith('userLogged='));

    if (!userLogged) {
        // Se o usuário não estiver logado, redirecionar para a página de login
        if (window.location.pathname !== '/Home/Loggin') { // Verificar se já não está na página de login
            window.location.href = '/Home/Loggin'; // Redireciona para a página de login
        }
    } else {
        // Caso o usuário esteja logado, redirecionar para a página principal (dashboard ou página inicial)
        if (window.location.pathname === '/Home/Loggin') { // Verificar se está na página de login
            window.location.href = '/Home/Dashboard'; // Redireciona para a página principal após login
        }
    }
}

// Executar a verificação quando a página for carregada
document.addEventListener('DOMContentLoaded', () => {
    checkLogin();
}); 
