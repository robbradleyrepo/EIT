var Shuffle = window.Shuffle;
class Demo {
  constructor(element) {
    this.element = element;
    this.shuffle = new Shuffle(element, {
      itemSelector: '.media-gallery__card',
      sizer: element.querySelector('.my-sizer-element')
    });
    // Log events.
    this.addShuffleEventListeners();
    this._activeFilters = [];
    this.addFilterButtons();
    this.addSorting();
    this.addSearchFilter();
  }
  /**
   * Shuffle uses the CustomEvent constructor to dispatch events. You can listen
   * for them like you normally would (with jQuery for example).
   */
  addShuffleEventListeners() {
    this.shuffle.on(Shuffle.EventType.LAYOUT, data => {
      console.log('layout. data:', data);
    });
    this.shuffle.on(Shuffle.EventType.REMOVED, data => {
      console.log('removed. data:', data);
    });
  }
  addFilterButtons() {
    const options = document.querySelector('.filter-gallery__options');
    if (!options) {
      return;
    }
    const filterButtons = Array.from(options.children);
    const onClick = this._handleFilterClick.bind(this);
    filterButtons.forEach(button => {
      button.addEventListener('click', onClick, false);
    });
  }
  _handleFilterClick(evt) {
    const btn = evt.currentTarget;
    const isActive = btn.classList.contains('active');
    const btnGroup = btn.getAttribute('data-group');
    this._removeActiveClassFromChildren(btn.parentNode);
    let filterGroup;
    if (isActive) {
      btn.classList.remove('active');
      btn.closest('.filter-gallery__group').classList.remove( 'OptionSlected' );
      filterGroup = Shuffle.ALL_ITEMS;
    } else {
      btn.classList.add('active');
      btn.closest('.filter-gallery__group').classList.add( 'OptionSlected' );
      filterGroup = btnGroup;
    }
    this.shuffle.filter(filterGroup);
  }
  _removeActiveClassFromChildren(parent) {
    const { children } = parent;
    for (let i = children.length - 1; i >= 0; i--) {
      children[i].classList.remove('active');
    }
  }
  addSorting() {
    const buttonGroup = document.querySelector('.sort-options');
    if (!buttonGroup) {
      return;
    }
    buttonGroup.addEventListener('change', this._handleSortChange.bind(this));
  }
  _handleSortChange(evt) {
    // Add and remove `active` class from buttons.
    const buttons = Array.from(evt.currentTarget.children);
    buttons.forEach(button => {
      if (button.querySelector('input').value === evt.target.value) {
        button.classList.add('active');
      } else {
        button.classList.remove('active');
      }
    });
    // Create the sort options to give to Shuffle.
    const { value } = evt.target;
    let options = {};
    function sortByDate(element) {
      return element.getAttribute('data-created');
    }
    function sortByTitle(element) {
      return element.getAttribute('data-title').toLowerCase();
    }
    if (value === 'date-created') {
      options = {
        reverse: true,
        by: sortByDate
      };
    } else if (value === 'title') {
      options = {
        by: sortByTitle
      };
    }
    this.shuffle.sort(options);
  }
  // Advanced filtering
  addSearchFilter() {
    const searchInput = document.querySelector('.filter-gallery__search-input');
    if (!searchInput) {
      return;
    }
    searchInput.addEventListener('keyup', this._handleSearchKeyup.bind(this));
  }
  /**
   * Filter the shuffle instance by items with a title that matches the search input.
   * @param {Event} evt Event object.
   */
  _handleSearchKeyup(evt) {
    const searchText = evt.target.value.toLowerCase();
    this.shuffle.filter((element, shuffle) => {
      // If there is a current filter applied, ignore elements that don't match it.
      if (shuffle.group !== Shuffle.ALL_ITEMS) {
        // Get the item's groups.
        const groups = JSON.parse(element.getAttribute('data-groups'));
        const isElementInCurrentGroup = groups.indexOf(shuffle.group) !== -1;
        // Only search elements in the current group
        if (!isElementInCurrentGroup) {
          return false;
        }
      }
      const titleElement = element.querySelector('.media-gallery__description');
      const titleText = titleElement.textContent.toLowerCase().trim();
      return titleText.indexOf(searchText) !== -1;
    });
  }
}
function filterActive(e) {
  if (document.querySelector('.filter-gallery__group.active') !== null) {
    document.querySelector('.filter-gallery__group.active').classList.remove('active');
    document.querySelector('.filter-gallery__group.active').childrens.forEach(function (e) {
      e.classList.remove('active');
    });
  }
  else {
    document.querySelector('.filter-gallery__group').classList.add('active');
    document.querySelector('.filter-gallery__group').childrens.forEach(function (e) {
      e.classList.add('active');
    });
  }
}
document.querySelector('.filter-gallery__group').addEventListener("click", filterActive);
document.addEventListener('DOMContentLoaded', () => {
  window.demo = new Demo(document.getElementById('mediaGallery'));
});
document.querySelectorAll('.select-all').forEach((el) => el.addEventListener("click", function() {
  var checkboxes = document.querySelectorAll('.media-gallery__controls .checkbox__input');
  for (var checkbox of checkboxes) {
    checkbox.checked = this.checked;
  }
  if (this.id === 'select-all-1') {
	document.getElementById('select-all-2').checked = this.checked;  
  }
  if (this.id === 'select-all-2') {
	document.getElementById('select-all-1').checked = this.checked;  
  }
}));
document.querySelectorAll('.media-gallery__download-selected').forEach((el) => el.addEventListener("click", function() {
  var checkboxes = document.querySelectorAll('.media-gallery__controls .checkbox__input');
  var chkArray = [];
  for (var checkbox of checkboxes) {
	  if(checkbox.checked) {		
		chkArray.push(checkbox.getAttribute('data-id'));
	  }
  }
  
  if (!chkArray.length)
  {
	  return;
  }
  
  var xhr = new XMLHttpRequest();
  xhr.open("POST", "MediaGalleryApi/DownloadMediaImages", true);
  xhr.setRequestHeader('Content-Type', 'application/json');
  xhr.responseType = "blob";
  xhr.onreadystatechange = function () {
	if (this.readyState == 4) {	 	  
		var blob = new Blob([this.response], { type: 'application/zip' });		
		let a = document.createElement("a");
		a.style = "display: none";
		document.body.appendChild(a);		
		let url = window.URL.createObjectURL(blob);
		a.href = url;		
		var fileName = '';
		var disposition = xhr.getResponseHeader('Content-Disposition');
		if (disposition && disposition.indexOf('attachment') !== -1) {
			var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
			var matches = filenameRegex.exec(disposition);
			if (matches != null && matches[1]) {
				fileName = matches[1].replace(/['"]/g, '');
			}
		}
		a.download = fileName;		
		a.click();
	}
  };
  xhr.send(JSON.stringify({
	downloadMediaIds: chkArray
  }));
}));