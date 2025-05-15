import { NavLink, useMatch, useResolvedPath } from 'react-router-dom'

function Navbar() {
  return (
    <nav className='fixed z-50 flex w-screen items-center justify-between bg-[#3b5253] px-5 py-4'>
      {/* Menu à esquerda */}
      <ul className='flex gap-4'>
        <li>
          <Link to={'/'}>Cadastro</Link>
        </li>
        <li>
          <Link to={'/usuario'}>Usuário</Link>
        </li>
        <li>
          <Link to={'/anuncios'}>Anúncios</Link>
        </li>
        <li>
          <Link to={'/criar_anuncio'}>Criar Anúncio</Link>
        </li>
      </ul>

      {/* Botão de Login à direita */}
      <ul className='flex gap-4'>
        <li>
          <Link to={'/login'}>Login</Link>
        </li>
      </ul>
    </nav>
  )
}

function Link({ to, children }) {
  const path = useResolvedPath(to).pathname
  const isActive = useMatch({ path: path, end: true })

  return (
    <NavLink
      className={`text-lg font-semibold no-underline transition-all duration-200 ${
        isActive
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
