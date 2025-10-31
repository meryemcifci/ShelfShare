// Simple client-side filtering by data-category
(function () {
  function onReady(fn) {
    if (document.readyState === 'loading') {
      document.addEventListener('DOMContentLoaded', fn);
    } else {
      fn();
    }
  }

  onReady(function () {
    var filterButtons = Array.prototype.slice.call(document.querySelectorAll('[data-filter]'));
    var cards = Array.prototype.slice.call(document.querySelectorAll('[data-category]'));

    function norm(v) {
      return (v || '').trim().toLowerCase();
    }

    function applyFilter(filter) {
      filter = norm(filter);
      cards.forEach(function (el) {
        var cat = norm(el.getAttribute('data-category'));
        if (!filter || filter === '*' || (cat && cat.indexOf(filter) !== -1)) {
          el.classList.remove('hidden');
        } else {
          el.classList.add('hidden');
        }
      });
    }

    filterButtons.forEach(function (btn) {
      btn.addEventListener('click', function () {
        var filter = norm(btn.getAttribute('data-filter'));
        applyFilter(filter);
        // active state styling (optional)
        filterButtons.forEach(function (b) { b.classList.add('secondary'); });
        btn.classList.remove('secondary');
      });
    });

    // Add-to-MyBooks button handler (placeholder)
    document.body.addEventListener('click', function (e) {
      var target = e.target;
      if (target && target.matches('[data-add-book-id]')) {
        e.preventDefault();
        var bookId = target.getAttribute('data-add-book-id');
        // TODO: Wire this to server endpoint when available.
        // fetch(`/Books/MyBooks/Add/${bookId}`, { method: 'POST' })
        //   .then(() => alert('Kitap eklendi'))
        //   .catch(() => alert('İşlem başarısız'));
        alert('Bu buton sunucuya bağlanınca MyBooks listesine ekleyecek. BookId: ' + bookId);
      }
    });
  });
})();


