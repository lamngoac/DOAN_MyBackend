<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" 
xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
xmlns:ex="http://exslt.org/dates-and-times"
xmlns:inv="http://laphoadon.gdt.gov.vn/2014/09/invoicexml/v1"
 extension-element-prefixes="ex">
<xsl:output method="html"/>
<xsl:template match="/">
	<html>
		<head>
			<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
			<title>Hóa Đơn Điện Tử</title>
			<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
      <meta http-equiv="X-UA-Compatible" content="IE=Edge"/>
      <link href="styleInvoiceXML.css" rel="stylesheet"/>      
      <script type="text/javascript" src="jquery-1.8.2.min.js"></script>
      <script type="text/javascript" src="details.js"></script>
      <script type="text/javascript" src="qrcode.js"></script>
      <script type="text/javascript" src="jquery.qrcode.js"></script>


      <!--<script type="text/javascript" src="jquery-1.8.2.min.js"></script>
			<script type="text/javascript" src="details.js"></script>
			<script type="text/javascript" src="qrcode.js"></script>
			<script type="text/javascript" src="jquery.qrcode.js"></script>	-->
		</head>
	 	<body>
			<div class="container">
				<div class="row-fluid">
				  <div class="span9">
					<div class="row-fluid">
						<div class="span4">
							<b>Mẫu số: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjTempInvoice/FormNo"/><br/>
							<b>Ký hiệu: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjTempInvoice/Sign"/><br/>
							<b>Số: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjInvoice/InvoiceNo"/>
						</div>
						<div class="span8">
							<p class="invName"><xsl:value-of select="'HÓA ĐƠN GIÁ TRỊ GIA TĂNG'"/></p>
							<p class="text-center">
								<xsl:if test="inv:viewData/inv:printFlag = 'true'">
									<i>(HÓA ĐƠN CHUYỂN ĐỔI TỪ HÓA ĐƠN XÁC THỰC)</i>
								</xsl:if>
								<br/>
								<i>
									<xsl:variable name="issuedDate" select="Invoice/InvoiceTemplate/ObjInvoice/InvoiceDateUTC"/>
									<xsl:value-of select="concat('Ngày ', substring($issuedDate, 9, 2), ' tháng ', substring($issuedDate, 6, 2), ' năm ', substring($issuedDate, 1, 4))"/>
								</i>
							</p>
						</div>	
					</div>	
					<div class="row-fluid">
						<div class="span12">
							<xsl:choose>
								<xsl:when test="inv:invoiceData/inv:adjustmentType = '3'"> 
									<b>Thay thế cho hóa đơn xác thực số: </b>
									<span class="border">
										<xsl:value-of select="inv:invoiceData/inv:originalInvoiceId"/>
									</span><br/>
								</xsl:when>
								<xsl:when test="inv:invoiceData/inv:adjustmentType = '5' and inv:invoiceData/inv:originalInvoiceId != ''"> 
									<b>Điều chỉnh cho hóa đơn xác thực số: </b>
									<span class="border">
										<xsl:value-of select="inv:invoiceData/inv:originalInvoiceId"/>
									</span><br/>
								</xsl:when>
								<xsl:when test="inv:invoiceData/inv:adjustmentType = '7' and inv:invoiceData/inv:originalInvoiceId != ''"> 
									<b>Xóa bỏ cho hóa đơn xác thực số: </b>
									<span class="border">
										<xsl:value-of select="inv:invoiceData/inv:originalInvoiceId"/>
									</span><br/>
								</xsl:when>
							</xsl:choose>
							<b>Số hóa đơn xác thực: </b><span class="border"><xsl:value-of select="inv:certifiedData/inv:certifiedId"/></span>
							<div><b>Mã xác thực: </b><span style="font-size: 11px;"><xsl:value-of select="inv:certifiedData/inv:signature"/></span></div>
						</div>
					</div>	
				  </div>
				  <div class="span3 text-right">
					<div id="qrcodeTable"></div>
				  </div>
				</div>
				
				<table style="margin-top:5px;" class="table table-bordered table-condensed">
					<tr>
						<td style="width:50%;">
							<fieldset>
								<legend>THÔNG TIN ĐƠN VỊ BÁN</legend>
								<div>
									<b>Đơn vị bán hàng: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstNNT/NNTFullName" disable-output-escaping="yes"/>
									<br/><b>Mã số thuế: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstNNT/MST"/>
									<br/><b>Địa chỉ: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstNNT/NNTAddress" disable-output-escaping="yes"/>
									<br/><b>Điện thoại: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstNNT/NNTPhone" disable-output-escaping="yes"/>
									<br/><b>Số tài khoản: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstNNT/AccNo" disable-output-escaping="yes"/>
									<xsl:choose>
										<xsl:when test="inv:invoiceData/inv:sellerBankName != '' "> 
											- <xsl:value-of select="inv:invoiceData/inv:sellerBankName" disable-output-escaping="yes"/>
										</xsl:when>
									</xsl:choose>
								</div>
							</fieldset>
						</td>
						<td style="width:50%;">
							<fieldset>
								<legend>THÔNG TIN KHÁCH HÀNG</legend>
								<div>
									<b>Họ tên người mua: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstCustomerNNT/ContactName" disable-output-escaping="yes"/>
									<br/><b>Tên đơn vị: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstCustomerNNT/CustomerNNTName" disable-output-escaping="yes"/>
									<br/><b>Mã số thuế: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstCustomerNNT/MST"/>
									<br/><b>Địa chỉ: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstCustomerNNT/CustomerNNTAddress" disable-output-escaping="yes"/>
									<br/><b>Số tài khoản: </b><xsl:value-of select="Invoice/InvoiceTemplate/ObjMstCustomerNNT/AccNo" disable-output-escaping="yes"/>
									<xsl:choose>
										<xsl:when test="inv:invoiceData/inv:buyerBankName != '' "> 
											-  <xsl:value-of select="inv:invoiceData/inv:buyerBankName" disable-output-escaping="yes"/>
										</xsl:when>
									</xsl:choose>
								</div>
							</fieldset>
						</td>
					</tr>
				</table>

				<div class="row-fluid">
					<div class="span6 text-center">
						<b>Phương thức thanh toán: </b><xsl:for-each select="inv:invoiceData/inv:payments/inv:payment">
															<xsl:value-of select="inv:paymentMethodName"/>
														</xsl:for-each>
					</div>
						<xsl:if test="inv:invoiceData/inv:exchangeRate != '' and inv:invoiceData/inv:exchangeRate != 0">
						<div class="span3 pull-right">
							<b>Tỷ giá: </b><span><xsl:value-of select="format-number(inv:invoiceData/inv:exchangeRate,'##.##0,##','number')"/>
											</span> 
						</div>
						</xsl:if>

					<div class="span3 pull-right">
						<b>Loại tiền: </b><xsl:value-of select="inv:invoiceData/inv:currencyCode"/>
					</div>
				</div>

				<table class="table table-bordered table-condensed">
					<tbody>
						<tr class="thead">
							<th>STT</th>
							<!-- <th>Mã hàng hóa, dịch vụ</th> -->
							<th style="width: 250px;">Tên hàng hóa, dịch vụ</th>
							<th>Đơn vị tính</th>
							<th>Số lượng</th>
							<th>Đơn giá trước thuế GTGT</th>
							<th>Thuế suất GTGT</th>
							<th>Thành tiền trước thuế GTGT</th>
						</tr>
						<tr class="thead">
							<th>(1)</th>
							<th>(2)</th>
							<th>(3)</th>
							<th>(4)</th>
							<th>(5)</th>
							<th>(6)</th>
							<th>(7=4x5)</th>
						</tr>
						<xsl:for-each select="Invoice/InvoiceTemplate/ListInvoiceDtl/ObjInvoiceDtl">
							<xsl:sort select="ObjInvoiceDtl" data-type="number"/>
							<tr>
								<td class="text-center"><xsl:value-of select="STT"/></td>
								<!-- <td><xsl:value-of select="inv:itemCode"/></td> -->
								<td>
									<xsl:choose>
										<xsl:when test="../../inv:adjustmentType = '5'"> 
											<xsl:choose>
												<xsl:when test="inv:isIncreaseItem = '' or inv:isIncreaseItem = 'true'">
													Điều chỉnh tăng:&#160;	
												</xsl:when>
												<xsl:otherwise>
													Điều chỉnh giảm:&#160;
							            		</xsl:otherwise>
											</xsl:choose>
										</xsl:when>
									</xsl:choose>
									<xsl:value-of select="ms_SpecName"/> 
						            <xsl:choose>
										<xsl:when test="inv:vatPercentage = '-1' "> 
											&#160;(không chịu thuế)
										</xsl:when>
										<xsl:when test="inv:vatPercentage = '-2' "> 
											&#160;(không kê khai nộp thuế)
										</xsl:when>
									</xsl:choose>
								</td>
								<td class="text-left"><xsl:value-of select="UnitCode"/></td>
								<td class="text-right">
									<xsl:if test="Qty != ''"> 
										<xsl:value-of select="format-number(Qty,'##.##0,##','number')"/>
									</xsl:if>
								</td>
								<td class="text-right">
									<xsl:if test="UnitPrice != ''">
										<xsl:value-of select="format-number(UnitPrice,'##.##0,##','number')"/>
									</xsl:if>
								</td>
								<xsl:choose>
									<xsl:when test="VATRate != '-1' and  VATRate != '-2'"> 
										<td class="text-right percentage"><xsl:value-of select="VATRate"/></td>
									</xsl:when>
									 <xsl:otherwise>
										<td></td>
									</xsl:otherwise>
								</xsl:choose>
								<td class="text-right">
									<xsl:if test="ThanhTien != ''">
										<xsl:value-of select="format-number(ThanhTien,'##.##0,##','number')"/>
									</xsl:if>
								</td>
							</tr>
						</xsl:for-each>
						<xsl:if test="inv:invoiceData/inv:discountAmount != '' and inv:invoiceData/inv:discountAmount != 0">
							<tr>
								<td class="text-center"><xsl:value-of select="count(inv:invoiceData/inv:items/inv:item) + 1"/></td>
								<td nowrap="">Chiết khấu</td>
								<td>
									<xsl:choose>
										<xsl:when test="inv:invoiceData/inv:adjustmentType = '5'"> 
											<xsl:choose>
												<xsl:when test="inv:invoiceData/inv:isDiscountAmtPos = 'true'">
													Điều chỉnh tăng	
												</xsl:when>
												<xsl:when test="inv:invoiceData/inv:isDiscountAmtPos = 'false'">
													Điều chỉnh giảm
							            		</xsl:when>
											</xsl:choose>
										</xsl:when>
									</xsl:choose>
								</td>
								<!-- <td class="text-left"></td> -->
								<td class="text-right"></td>
								<td class="text-right"></td>
								<td class="text-right"></td>
								<td class="text-right">
									<xsl:value-of select="format-number(inv:invoiceData/inv:discountAmount,'##.##0,##','number')"/>
								</td>
							</tr>
						</xsl:if>
					</tbody>
				</table>

				<table style="margin-top:15px;" class="table table-bordered table-condensed">
				   <tbody>
						<tr class="thead">
							<th class="text-center">Loại thuế suất</th>
							<xsl:if test="inv:invoiceData/inv:exchangeRate != '' and inv:invoiceData/inv:exchangeRate != 0">
								<th class="text-center">Ngoại tệ</th>
							</xsl:if>
							<th class="text-center">Tiền thuế GTGT</th>
							<th class="text-center">Thành tiền VNĐ</th>
						</tr>
						<xsl:choose>
							<xsl:when test="inv:invoiceData/inv:exchangeRate != '' and inv:invoiceData/inv:exchangeRate != 0">
								<xsl:for-each select="inv:invoiceData/inv:invoiceTaxBreakdowns/inv:invoiceTaxBreakdown">
									<tr>
										<td>
											<xsl:choose>
												<xsl:when test="(../../inv:adjustmentType = '5' or ../../inv:adjustmentType = '9') and ../../inv:totalVATAmount != '0'"> 
													<xsl:choose>
														<xsl:when test="inv:isVatTaxAmountPos != '' and inv:isVatTaxAmountPos != 'true'">
															Điều chỉnh giảm:&#160;
														</xsl:when>
														<xsl:otherwise>
															Điều chỉnh tăng:&#160;	
									            		</xsl:otherwise>
													</xsl:choose> hàng hóa DV
												</xsl:when>
												<xsl:otherwise>
													Hàng hóa DV
												</xsl:otherwise>
											</xsl:choose>
											<xsl:choose>
												<xsl:when test="inv:vatPercentage = '-1'">
													không chịu thuế
												</xsl:when>
												<xsl:when test="inv:vatPercentage = '-2'">
													không kê khai nộp thuế
												</xsl:when>
												<xsl:otherwise>
													<span class="percentage"><xsl:value-of select="inv:vatPercentage"/></span>
												</xsl:otherwise>
											</xsl:choose>
										</td>
										<td></td>
										<td class="text-right">
											<xsl:if test="inv:vatTaxAmount != ''">
												<xsl:value-of select="format-number(inv:vatTaxAmount,'##.##0,##','number')"/>
											</xsl:if>
										</td>
										<td class="text-right">
											<xsl:if test="inv:vatTaxableAmount != ''">
												<xsl:value-of select="format-number(inv:vatTaxableAmount,'##.##0,##','number')"/>
											</xsl:if>
										</td>
									</tr>
								</xsl:for-each>
							</xsl:when>
							<xsl:otherwise>
								<xsl:for-each select="inv:invoiceData/inv:invoiceTaxBreakdowns/inv:invoiceTaxBreakdown">
									<tr>
										<td>
											<xsl:choose>
												<xsl:when test="(../../inv:adjustmentType = '5' or ../../inv:adjustmentType = '9') and ../../inv:totalVATAmount != '0'"> 
													<xsl:choose>
														<xsl:when test="inv:isVatTaxAmountPos != '' and inv:isVatTaxAmountPos != 'true'">
															Điều chỉnh giảm:&#160;
														</xsl:when>
														<xsl:otherwise>
															Điều chỉnh tăng:&#160;	
									            		</xsl:otherwise>
													</xsl:choose> hàng hóa DV
												</xsl:when>
												<xsl:otherwise>
													Hàng hóa DV
												</xsl:otherwise>
											</xsl:choose>
											 <xsl:choose>
												<xsl:when test="inv:vatPercentage = '-1'">
													không chịu thuế
												</xsl:when>
												<xsl:when test="inv:vatPercentage = '-2'">
													không kê khai nộp thuế
												</xsl:when>
												<xsl:otherwise>
													<span class="percentage"><xsl:value-of select="inv:vatPercentage"/></span>
												</xsl:otherwise>
											</xsl:choose> 
										</td>
										<td class="text-right">
											<xsl:if test="inv:vatTaxAmount != ''">
										 		<xsl:value-of select="format-number(inv:vatTaxAmount,'##.##0,##','number')"/>
										 	</xsl:if>
									 	</td>
										<td class="text-right">
											<xsl:if test="inv:vatTaxableAmount != ''">
												<xsl:value-of select="format-number(inv:vatTaxableAmount,'##.##0,##','number')"/>
											</xsl:if>
										</td>
									</tr>
								</xsl:for-each>
							</xsl:otherwise>
						</xsl:choose>
						<tr>
							<td>
								<xsl:choose>
									<xsl:when test="(inv:invoiceData/inv:adjustmentType = '5' or inv:invoiceData/inv:adjustmentType = '9') and inv:invoiceData/inv:totalVATAmount != '0'"> 
										<xsl:choose>
											<xsl:when test="inv:invoiceData/inv:isTotalVATAmountPos = '' or inv:invoiceData/inv:isTotalVATAmountPos = 'true'">
												Điều chỉnh tăng:&#160;	
											</xsl:when>
											<xsl:otherwise>
												Điều chỉnh giảm:&#160;
						            		</xsl:otherwise>
										</xsl:choose> tổng tiền thuế GTGT
									</xsl:when>
									<xsl:otherwise>
										Tổng tiền thuế GTGT
									</xsl:otherwise>
								</xsl:choose>
							</td>
							<xsl:if test="inv:invoiceData/inv:exchangeRate != '' and inv:invoiceData/inv:exchangeRate != 0">
								<td></td>
							</xsl:if>
							<td class="text-right">
							 	<xsl:if test="Invoice/InvoiceTemplate/ObjInvoice/TongTienThue != ''">
							 		<xsl:value-of select="format-number(Invoice/InvoiceTemplate/ObjInvoice/TongTienThue,'##.##0,##','number')"/>
							 	</xsl:if>
							</td>
							<td></td>
						</tr>
						<tr>
							<td>
								<xsl:choose>
									<xsl:when test="(inv:invoiceData/inv:adjustmentType = '5' or inv:invoiceData/inv:adjustmentType = '9') and inv:invoiceData/inv:totalAmountWithVAT != '0'"> 
										<xsl:choose>
											<xsl:when test="inv:invoiceData/inv:isTotalAmountPos = '' or inv:invoiceData/inv:isTotalAmountPos = 'true'">
												Điều chỉnh tăng:&#160;	
											</xsl:when>
											<xsl:otherwise>
												Điều chỉnh giảm:&#160;
						            		</xsl:otherwise>
										</xsl:choose> tổng tiền thanh toán:
									</xsl:when>
									<xsl:otherwise>
										Tổng tiền thanh toán:
									</xsl:otherwise>
								</xsl:choose>
							</td>
								<xsl:choose>
								<xsl:when test="inv:invoiceData/inv:exchangeRate != '' and inv:invoiceData/inv:exchangeRate != 0">
									<td class="text-right">
										<xsl:if test="inv:invoiceData/inv:totalAmountWithVATFrn != ''">
											<xsl:value-of select="format-number(inv:invoiceData/inv:totalAmountWithVATFrn,'##.##0,##','number')"/>
										</xsl:if>
									</td>
								</xsl:when>
							</xsl:choose>
							<td></td>
							<td class="text-right">
								<xsl:if test="Invoice/InvoiceTemplate/ObjInvoice/TongTienCoThue != ''">
									<xsl:value-of select="format-number(Invoice/InvoiceTemplate/ObjInvoice/TongTienCoThue,'##.##0,##','number')"/>
								</xsl:if>
							</td>
						</tr>
				   </tbody>
				</table>
				
				<div class="row-fluid">
				  <div class="span12">
					<b>Số tiền viết bằng chữ: <i><xsl:value-of select="Invoice/InvoiceTemplate/ObjInvoice/SoTienBangChu"/></i></b>
						<xsl:if test="inv:invoiceData/inv:invoiceNote != ''">
							<br/><b>Ghi chú hóa đơn:  </b><xsl:value-of select="inv:invoiceData/inv:invoiceNote"/>
						</xsl:if>
				  </div>
				</div>
				
				<xsl:choose>
					<xsl:when test="inv:viewData/inv:printFlag = 'true'">
						<div class="row-fluid">
							<div class="span4">
								<p class="text-center">
									Người thực hiện chuyển đổi<br/>
									(Ký, ghi rõ họ, tên)
								</p>
								<br/><br/>
							</div>
						  	<div class="span4">
								<p class="text-center">
									Người mua hàng<br/>
									(Ký, ghi rõ họ, tên)
								</p>
								<br/><br/>
						  	</div>
						  	<div class="span4">
								<p class="text-center">
									Người bán hàng<br/>
									(Ký, đóng dấu, ghi rõ họ, tên)
								</p>
								<br/><br/>
						  	</div>
						</div>
						<div class="row-fluid">
							<div class="span4">
								<p class="text-center">
									<xsl:value-of select="inv:viewData/inv:userPrint"/><br/>
								</p>
								<p class="text-center">
				                    Ngày chuyển đổi: 
									<i>
										<xsl:choose>
											<xsl:when test="inv:viewData/inv:userPrint != ''">
												<xsl:value-of select="concat(substring(ex:date-time(), 9, 2),
														'/', substring(ex:date-time(), 6, 2),
														'/', substring(ex:date-time(), 1, 4))" />
											</xsl:when>
											<xsl:otherwise>
												&#160;&#160;&#160;&#160;/&#160;&#160;&#160;&#160;/20
											</xsl:otherwise>
										</xsl:choose>
									</i>
								</p>
							</div>
							<div class="span4"></div>
							<div class="span4">
								<xsl:if test="Invoice/CKyDTu/KeyInfo/X509Data/X509SubjectName != ''">
									<p class="text-center">
										Được ký bởi <xsl:value-of select="Invoice/CKyDTu/KeyInfo/X509Data/X509SubjectName"/>
										<!-- <br/>
										<p class="text-center">
						                    Ngày ký: 
											<i>
												<xsl:variable name="signedDate" select="inv:invoiceData/inv:signedDate"/>
												<xsl:value-of select="concat('Ngày ', substring($signedDate, 9, 2), ' tháng ', substring($signedDate, 6, 2), ' năm ', substring($signedDate, 1, 4))"/>
											</i>
										</p> -->
									</p>
								</xsl:if>
							</div>
						</div>
					</xsl:when>
					<xsl:otherwise>
						<div class="row-fluid">
							<div class="span6">
								<p class="text-center">
									Người mua hàng<br/>
									(Ký, ghi rõ họ, tên)
								</p>
								<br/><br/>
						  	</div>
						  	<div class="span6">
								<p class="text-center">
									Người bán hàng<br/>
									(Ký, đóng dấu, ghi rõ họ, tên)
								</p>
								<br/><br/>
						  	</div>
					  	</div>
					  	<div class="row-fluid">
							<div class="span6"></div>
							<div class="span6">
								<xsl:if test="Invoice/CKyDTu/Signature/KeyInfo/X509Data/X509SubjectName != ''">
									<p class="text-center">
										Được ký bởi <xsl:value-of select="Invoice/CKyDTu/Signature/KeyInfo/X509Data/X509SubjectName"/>
										<!-- <br/>
										<p class="text-center">
						                    Ngày ký: 
											<i>
												<xsl:variable name="signedDate" select="inv:invoiceData/inv:signedDate"/>
												<xsl:value-of select="concat('Ngày ', substring($signedDate, 9, 2), ' tháng ', substring($signedDate, 6, 2), ' năm ', substring($signedDate, 1, 4))"/>
											</i>
										</p> -->
									</p>
								</xsl:if>
							</div>
						</div>
					</xsl:otherwise>
				</xsl:choose>

				<p class="text-center footer">
					(Cần kiểm tra, đối chiếu khi lập, giao, nhận hóa đơn)<br/>
					<xsl:if test="inv:invoiceData/inv:signedDate != ''">Hóa đơn điện tử được cấp mã xác thực bởi Tổng cục Thuế Việt Nam</xsl:if>
				</p>

				<input type="hidden" id="qrcodeContent">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:controlData/inv:qrCodeData" />
					</xsl:attribute>
				</input>
				<input id="InvoiceID" name="InvoiceID" type="hidden">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:certifiedData/inv:certifiedId" />
					</xsl:attribute>
				</input> 
				<input id="SellerTaxCode" name="SellerTaxCode" type="hidden">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:invoiceData/inv:sellerTaxCode" />
					</xsl:attribute>
				</input>
				<input id="BuyerTaxCode" name="BuyerTaxCode" type="hidden">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:invoiceData/inv:buyerTaxCode" />
					</xsl:attribute>
				</input> 
				<input id="InvoiceIssuedDate" name="InvoiceIssuedDate" type="hidden">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:invoiceData/inv:invoiceIssuedDate" />
					</xsl:attribute>
				</input>
				<input id="OriginalInvoiceId" name="OriginalInvoiceId" type="hidden">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:invoiceData/inv:originalInvoiceId" />
					</xsl:attribute>
				</input> 
				<input id="TotalVATAmount" name="TotalVATAmount" type="hidden">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:invoiceData/inv:totalVATAmount" />
					</xsl:attribute>
				</input> 
				<input id="AmountForPayment" name="AmountForPayment" type="hidden">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:invoiceData/inv:totalAmountWithVAT" />
					</xsl:attribute>
				</input> 
			   <input type="hidden" id="AntiFraudCode" name="AntiFraudCode">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:certifiedData/inv:signature" />
					</xsl:attribute>
				</input>
				<input type="hidden" id="SellerSignature" name="SellerSignature">
					<xsl:attribute name="value">
						<xsl:value-of select="inv:certifiedData/inv:sellerSignatureHash" />
					</xsl:attribute>
				</input> 

			</div>
		</body>
	</html>
</xsl:template>
<xsl:decimal-format name="number" decimal-separator="," grouping-separator="."/>
</xsl:stylesheet>
