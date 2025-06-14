import {
  NavLink,
  useMatch,
  useNavigate,
  useResolvedPath,
} from 'react-router-dom'

function Navbar() {
  const isLoggedIn = !!localStorage.getItem('token')
  const redirect = useNavigate()

  return (
    <nav className='fixed z-50 flex w-screen items-center justify-between bg-[#3b5253] px-5 py-4'>
      <ul className='flex gap-4'>
        {!isLoggedIn && (
          <>
            <li>
              <Link to={'/login'}>Login</Link>
            </li>
            <li>
              <Link to={'/cadastro'}>Cadastro</Link>
            </li>
          </>
        )}

        {isLoggedIn && (
          <>
            <li>
              <Link to={'/usuario'}>Usuário</Link>
            </li>
            <li>
              <Link to={'/anuncios'}>Anúncios</Link>
            </li>
            <li>
              <Link to={'/favoritos'}>Favoritos</Link>
            </li>
            <li>
              <Link to={'/denunciados'}>Denunciados</Link>
            </li>
            <li>
              <Link to={'/criar_anuncio'}>Criar Anúncio</Link>
            </li>
          </>
        )}
      </ul>

      <ul className='flex items-center gap-4'>
        {isLoggedIn && (
          <li>
            <button
              onClick={() => {
                localStorage.removeItem('token')
                localStorage.removeItem('userId')
                redirect('/login')
              }}
              className='cursor-pointer text-lg font-semibold text-white no-underline transition-all duration-200 hover:text-[#bddcd8]'
            >
              Sair
            </button>
          </li>
        )}
      </ul>
    </nav>
  )
}

function Link({ to, children }) {
  const path = useResolvedPath(to).pathname
  const isMatch = useMatch({ path: path, end: true })

  return (
    <NavLink
      className={`text-lg font-semibold no-underline transition-all duration-200 ${
        isMatch
          ? 'border-b-4 border-[#bddcd8] py-1.5 text-[#bddcd8]'
          : 'text-white hover:text-[#bddcd8]'
      }`}
      to={to}
    >
      {children}
    </NavLink>
  )
}

export { Navbar }
