export function init() {
  const sidebarToggles = document.body.querySelectorAll('[data-toggle="sidebar"]');
  sidebarToggles.forEach((toggle) => toggle.addEventListener('click', e => {
    toggleSidebar(!document.body.classList.contains('sidebar-toggled'));
  }))
  
  const sidebarBackdrop = document.querySelector('.sidebar-backdrop');
  if (sidebarBackdrop) {
    sidebarBackdrop.addEventListener('click', event => {
      toggleSidebar(false);
    })
  }
}

export function toggleSidebar(toggle) {
  if (toggle) {
    document.body.classList.add('sidebar-toggled');
  } else {
    document.body.classList.remove('sidebar-toggled');
  }
}