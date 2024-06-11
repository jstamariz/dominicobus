(() => {
	window.getSelectedValues = (selectControl) => {
		let results = [];
		Array.from(selectControl.options).forEach((option) => {
			if (option.selected) {
				results.push(option.value);
			}
		});
		return results;
	};
})();
