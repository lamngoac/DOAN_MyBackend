/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';

    config.filebrowserBrowseUrl = '/Content/plugin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Content/plugin/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Content/plugin/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Content/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Content/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/Content/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
};
